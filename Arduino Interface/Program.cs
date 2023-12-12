using Arduino_Interface;
using SimpleHttp;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
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


            // Start the simpleHttp server on the main thread
            Task.Run(() => StartHttpServer());

            // Start your WinForms application in the main thread
            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
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

                // Use Control.Invoke to safely update UI elements
                formInstance.Invoke((MethodInvoker)delegate
                {
                    // Update the TextBox with the received text
                    formInstance.UpdateTextBox(textToWrite);
                });

                res.AsText($"You wrote {textToWrite}");
            });

            Route.Add("/capture-frame", (req, res, prop) =>
            {
               

              
            });

            Route.Add("/command/{string}", (req, res, prop) =>
            {
                string textToWrite = prop["string"];


                // Call the WebCapture method from your WinForms application
                formInstance.CommandString(textToWrite);

                // Send a response indicating success
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