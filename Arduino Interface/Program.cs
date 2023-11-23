using SimpleHttp;
using System;
using System.Collections.Generic;
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

            Route.Add("/users/{id}", (req, res, prop) =>
            {
                res.AsText($"You requested User {prop["id"]}");
            });

            Route.Add("/capture-frame", (req, res, prop) =>
            {
                if (req.HttpMethod == "POST")
                {
                    // Call the WebCapture method from your WinForms application
                    formInstance.WebCapture();

                    // Send a response indicating success
                    res.AsText("Frame captured successfully.");
                }
                else
                {
                    res.StatusCode = 404;
                    res.AsText("Not Found");
                }
            }, "POST");

            HttpServer.ListenAsync(1337, CancellationToken.None, Route.OnHttpRequestAsync).Wait();
        }
    }
}