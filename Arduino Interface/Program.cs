using Arduino_Interface;
using SimpleHttp;
using System;
using System.IO;
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

            Route.Add("/capture-frame", (req, res, prop) =>
            {
                formInstance.WebCapture();
                string imageDirectory = @"C:\Users\USER PC\Pictures\Camera Roll\";


                int frameCount = formInstance.frameCountPublic;
                string filename = Path.Combine(imageDirectory, $"captured_frame_{frameCount - 1}.jpeg");

                if (File.Exists(filename))
                {

                    byte[] imageBytes = File.ReadAllBytes(filename);


                    res.OutputStream.Write(imageBytes, 0, imageBytes.Length);


                    res.ContentType = "image/jpeg";


                    res.StatusCode = 200;
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