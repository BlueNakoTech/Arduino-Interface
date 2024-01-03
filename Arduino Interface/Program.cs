using Arduino_Interface;
using SimpleHttp;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arduino_Interface
{
    internal static class Program
    {
        private static Form1 formInstance;

        [STAThread]
        static void Main(string[] args)
        {

            formInstance = new Form1();

            Task.Run(() => StartHttpServer());


            Application.EnableVisualStyles();

            Application.Run(formInstance);
        }

        static void StartHttpServer()
        {
            Route.Add("/", (req, res, prop) =>
            {
                res.AsText("Welcome to Home");
            });

            Route.Add("/write/{string}", (req, res, prop) =>
            {
                string textToWrite = prop["string"];


                formInstance.Invoke((MethodInvoker)delegate
                {
                    // Update the TextBox with the received text
                    formInstance.UpdateTextBox(textToWrite);
                });

                res.AsText($"You wrote {textToWrite}");
            });

            Route.Add("/capture-frame", async (req, res, prop) =>
            {
                formInstance.WebCapture();
                string imageDirectory = @"C:\Users\USER PC\Pictures\Camera Roll\";
                int frameCount = formInstance.frameCountPublic;
                string filename = Path.Combine(imageDirectory, $"captured_frame_{frameCount - 1}.jpeg");

                if (File.Exists(filename))
                {
                    try
                    {
                        using (FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read))
                        {
                            byte[] imageBytes = new byte[fileStream.Length];
                            await fileStream.ReadAsync(imageBytes, 0, imageBytes.Length);

                            // Convert the image bytes to a base64 string
                            string base64Image = Convert.ToBase64String(imageBytes);

                            // Send the base64 string in the response
                            res.ContentType = "text/plain";
                            await res.OutputStream.WriteAsync(Encoding.UTF8.GetBytes(base64Image), 0, base64Image.Length);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        res.StatusCode = 500;
                        res.AsText("Internal Server Error");
                    }
                    finally
                    {
                        // Ensure the response is complete by closing the output stream
                        res.OutputStream.Close();
                    }
                }


                else
                {

                    res.StatusCode = 404;
                    res.AsText("Image not found.");
                }


            });

            Route.Add("/command/{string}", (req, res, prop) =>
            {
                string textToWrite = prop["string"];



                formInstance.CommandString(textToWrite);


                res.AsText("command receive");



            });

            HttpServer.ListenAsync(1337, CancellationToken.None, Route.OnHttpRequestAsync).Wait();
        }



    }
}
internal class CommandProcessor
{
    private readonly Form1 formInstance;

    public CommandProcessor(Form1 formInstance)
    {
        this.formInstance = formInstance;
    }

    public void ProcessCommand(string command)
    {
        switch (command)
        {
            case "capture-frame":
                formInstance.WebCapture();
                break;

            case "some-other-command":
                // Handle another command
                break;

            // Add more cases as needed

            default:
                // Handle unknown commands
                break;
        }
    }
}