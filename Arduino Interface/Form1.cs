using AForge.Imaging;
using AForge.Video;
using AForge.Video.DirectShow;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tesseract;
using Image = System.Drawing.Image;
using ImageFormat = System.Drawing.Imaging.ImageFormat;

namespace Arduino_Interface
{



    public partial class Form1 : Form
    {
        private string receivedDataBuffer = "";
        private SerialPort serialPort;
        private FirebaseClient firebaseClient;
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;
        private Bitmap currentFrame;
        private List<Bitmap> capturedFrames;
        private int frameCount = 0;
        public PictureBox saved2Display;
        public int frameCountPublic = 0;


        public Form1()
        {
            InitializeComponent();
            InitializeWebcam();

            capturedFrames = new List<Bitmap>();
            string[] availablePorts = SerialPort.GetPortNames();
            comboBoxPort.Items.AddRange(availablePorts);
            firebaseClient = new FirebaseClient("https://imagedata-3ddf4-default-rtdb.asia-southeast1.firebasedatabase.app/");

        }


        private void GetWebcamCapabilities()
        {
            if (videoSource != null && videoSource.VideoCapabilities.Length > 0)
            {
                foreach (var capability in videoSource.VideoCapabilities)
                {
                    Console.WriteLine($"Resolution: {capability.FrameSize}, Aspect Ratio: {capability.FrameSize.Width}:{capability.FrameSize.Height}");
                }
            }
        }

        private void SetDesiredResolution(VideoCaptureDevice videoSource, int width, int height)
        {
            if (videoSource != null && videoSource.VideoCapabilities.Length > 0)
            {

                var desiredCapability = videoSource.VideoCapabilities.FirstOrDefault(cap => cap.FrameSize.Width == width && cap.FrameSize.Height == height);
                if (desiredCapability != null)
                {
                    videoSource.VideoResolution = desiredCapability;
                }
            }
        }


        private void InitializeWebcam()
        {
            try
            {
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                if (videoDevices.Count == 0)
                {
                    MessageBox.Show("No video devices found.");
                    return;
                }

                videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);


                SetDesiredResolution(videoSource, 1280, 720);
                SetDesiredAspectRatio(videoSource, 4.0 / 3.0);

                videoSource.NewFrame += new NewFrameEventHandler(videoSource_NewFrame);
                videoSource.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error initializing webcam: " + ex.Message);
            }
        }
        private void SetDesiredAspectRatio(VideoCaptureDevice videoSource, double aspectRatio)
        {
            if (videoSource != null && videoSource.VideoCapabilities.Length > 0)
            {

                var desiredCapability = videoSource.VideoCapabilities.FirstOrDefault(cap => Math.Abs((double)cap.FrameSize.Width / cap.FrameSize.Height - aspectRatio) < 0.01);
                if (desiredCapability != null)
                {
                    videoSource.VideoResolution = desiredCapability;
                }
            }
        }


        private bool IsSerialPortOpen()
        {
            return serialPort != null && serialPort.IsOpen;
        }
        private void ShowSerialPortErrorMessage()
        {
            MessageBox.Show("Serial port is not open. Connect to the Arduino first.");
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string receivedData = serialPort.ReadExisting();
            receivedDataBuffer += receivedData;


            AppendDataToSerialMonitor(receivedData);
        }


        private void AppendDataToSerialMonitor(string data)
        {
            string currentTime = DateTime.Now.ToString("HH:mm:ss");
            string formattedData = $"# [{currentTime}] - {data}\n";

            if (serialMonitorTextBox.InvokeRequired)
            {
                serialMonitorTextBox.Invoke(new Action<string>(AppendDataToSerialMonitor), new object[] { data });
            }
            else
            {
                bool shouldAutoScroll = IsAutoScrollEnabled(serialMonitorTextBox);


                serialMonitorTextBox.AppendText(formattedData);

                if (shouldAutoScroll)
                {
                    serialMonitorTextBox.ScrollToCaret();
                }
            }
        }

        private bool IsAutoScrollEnabled(RichTextBox richTextBox)
        {
            int visibleLines = richTextBox.ClientSize.Height / richTextBox.Font.Height;
            return richTextBox.Lines.Length - richTextBox.GetLineFromCharIndex(richTextBox.SelectionStart) <= visibleLines;
        }

        private void online_Click(object sender, EventArgs e)
        {
            string selectedPort = comboBoxPort.SelectedItem.ToString();
            serialPort = new SerialPort(selectedPort, 115200);
            try
            {
                serialPort.Open();
                if (serialPort.IsOpen)
                {
                    serialPort.Write("online");
                    serialPort.DataReceived += SerialPort_DataReceived;
                }
                else
                {
                    MessageBox.Show("Failed to open the serial port.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void offline_Click(object sender, EventArgs e)
        {
            if (serialPort != null && serialPort.IsOpen)
            {
                serialPort.DataReceived -= SerialPort_DataReceived;
                serialPort.Close();

                serialPort.Dispose();
            }
        }


        private void onButton_Click(object sender, EventArgs e)
        {
            if (IsSerialPortOpen())
            {
                string inputText = "a123789h";
                string currentTime = DateTime.Now.ToString("HH:mm:ss");
                string formattedData = $"# [{currentTime}] - {inputText}\n";
                for (int i = 0; i < inputText.Length; i++)
                {
                    char c = inputText[i];
                    serialPort.Write(c.ToString());

                    while (!receivedDataBuffer.Contains("d"))
                    {
                        Application.DoEvents();
                    }

                    receivedDataBuffer = "";

                    if (i == inputText.Length - 1)
                    {

                    }
                }
            }
            else
            {
                ShowSerialPortErrorMessage();
            }
        }





        private void sendButton_Click(object sender, EventArgs e)
        {
            if (IsSerialPortOpen())
            {
                string inputText = inputBox.Text;
                string currentTime = DateTime.Now.ToString("HH:mm:ss");
                string formattedData = $"# [{currentTime}] - {inputText}\n";
                for (int i = 0; i < inputText.Length; i++)
                {
                    char c = inputText[i];
                    serialPort.Write(c.ToString());

                    while (!receivedDataBuffer.Contains("d"))
                    {
                        Application.DoEvents();
                    }


                    receivedDataBuffer = "";

                    if (i == inputText.Length - 1)
                    {


                        serialMonitorTextBox.AppendText(formattedData);
                        inputBox.Clear();
                        serialPort.Write("a");
                    }
                }
            }
            else
            {
                ShowSerialPortErrorMessage();
            }
        }




        private string PerformOCR(string imagePath)
        {
            using (var engine = new TesseractEngine("./tessdata", "seg", EngineMode.TesseractOnly))
            {
                using (var img = Pix.LoadFromFile(imagePath))
                {
                    using (var page = engine.Process(img))
                    {
                        return page.GetText();
                    }
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (IsSerialPortOpen())
            {
                string inputText = "a124681";
                string currentTime = DateTime.Now.ToString("HH:mm:ss");
                string formattedData = $"# [{currentTime}] - Check Complete\n";
                for (int i = 0; i < inputText.Length; i++)
                {
                    char c = inputText[i];
                    serialPort.Write(c.ToString());

                    while (!receivedDataBuffer.Contains("d"))
                    {

                        Application.DoEvents();
                    }


                    receivedDataBuffer = "";


                    if (i == inputText.Length - 1)
                    {

                        serialMonitorTextBox.AppendText(formattedData);
                        inputBox.Clear();

                    }
                }
            }
            else
            {
                ShowSerialPortErrorMessage();
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (IsSerialPortOpen())
            {
                string inputText = "a123789c";
                string currentTime = DateTime.Now.ToString("HH:mm:ss");
                string formattedData = $"# [{currentTime}] - Check Complete\n";
                for (int i = 0; i < inputText.Length; i++)
                {
                    char c = inputText[i];
                    serialPort.Write(c.ToString());

                    while (!receivedDataBuffer.Contains("d"))
                    {

                        Application.DoEvents();
                    }


                    receivedDataBuffer = "";


                    if (i == inputText.Length - 1)
                    {

                        serialMonitorTextBox.AppendText(formattedData);
                        inputBox.Clear();

                    }
                }
            }
            else
            {
                ShowSerialPortErrorMessage();
            }

        }


        private async void button13_Click(object sender, EventArgs e)
        {
            if (IsSerialPortOpen())
            {
                string inputText = "a1237891c";
                string currentTime = DateTime.Now.ToString("HH:mm:ss");
                string formattedData = $"# [{currentTime}] - Check Complete\n";
                int timeoutMilliseconds = 30000;
                DateTime startTime = DateTime.Now;
                for (int i = 0; i < inputText.Length; i++)
                {
                    char c = inputText[i];
                    serialPort.Write(c.ToString());

                    while (!receivedDataBuffer.Contains("d"))
                    {

                        Application.DoEvents();
                    }


                    receivedDataBuffer = "";


                    if (i == inputText.Length - 1)
                    {
                     
                        if ((DateTime.Now - startTime).TotalMilliseconds >= timeoutMilliseconds)
                        {
                           
                            
                            inputText = "a1c"; 
                        }
                        await Task.Delay(1000);
                        WebCapture();
                        await HomeAsync();

                        
                        serialMonitorTextBox.AppendText(formattedData);
                        inputBox.Clear();
                    }
                }
            }
            else
            {
                ShowSerialPortErrorMessage();
            }

        }




        private void videoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (currentFrame != null)
            {
                currentFrame.Dispose();
            }
            currentFrame = (Bitmap)eventArgs.Frame.Clone();
            currentFrame = new Bitmap(currentFrame, currentDisplay.Size);

            currentDisplay.Image = currentFrame;
        }

        public void WebCapture()
        {
            CaptureFrame();
            LoadLastSavedFrame();
            frameCount++;
           // frameCountPublic++;
           
        }

        public void CommandString(string command)
        {
            if (IsSerialPortOpen())
            {
                string inputText = command;
                string currentTime = DateTime.Now.ToString("HH:mm:ss");
                string formattedData = $"# [{currentTime}] - Check Complete\n";
                for (int i = 0; i < inputText.Length; i++)
                {
                    char c = inputText[i];
                    serialPort.Write(c.ToString());

                    while (!receivedDataBuffer.Contains("d"))
                    {

                        Application.DoEvents();
                    }


                    receivedDataBuffer = "";

                }
            }
            else
            {
                ShowSerialPortErrorMessage();
            }
        }
        public void UpdateTextBox(string newText)
        {
            if (serialMonitorTextBox.InvokeRequired)
            {
                // If called from a different thread, invoke on the UI thread
                serialMonitorTextBox.Invoke(new Action(() => UpdateTextBox(newText)));
            }
            else
            {
                // Update the TextBox
                serialMonitorTextBox.Text = newText;
            }
        }

        private void CaptureFrame()
        {
            if (currentDisplay.Image != null)
            {

                Bitmap capturedFrame32bpp = new Bitmap(currentDisplay.Image);


                Bitmap capturedFrame24bpp = new Bitmap(capturedFrame32bpp.Width, capturedFrame32bpp.Height, PixelFormat.Format24bppRgb);

                using (Graphics graphics = Graphics.FromImage(capturedFrame24bpp))
                {
                    graphics.DrawImage(capturedFrame32bpp, new Rectangle(0, 0, capturedFrame24bpp.Width, capturedFrame24bpp.Height));
                }


                string filename = @"C:\Users\USER PC\Pictures\Camera Roll\captured_frame_" + frameCount + ".jpeg";
                capturedFrame24bpp.Save(filename, ImageFormat.Jpeg);


                capturedFrame24bpp.Dispose();
                capturedFrame32bpp.Dispose();

                capturedFrames.Add(capturedFrame24bpp);

            }
        }



        private void LoadLastSavedFrame()
        {
            string filename = @"C:\Users\USER PC\Pictures\Camera Roll\captured_frame_" + frameCount + ".jpeg";

            if (File.Exists(filename))
            {
                Image image = Image.FromFile(filename);

                savedDisplay.Image = image;
            }
            else
            {
                MessageBox.Show("No frame found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void LoadSavedFrame()
        {
            string filename = @"C:\Users\USER PC\Pictures\Camera Roll\captured_frame_" + frameCount + ".jpeg";

            if (File.Exists(filename))
            {
                Image image = Image.FromFile(filename);

                savedDisplay.Image = image;
            }
            else
            {
                MessageBox.Show("No frame found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            CaptureFrame();
            LoadLastSavedFrame();
            frameCount++;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource.WaitForStop();
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (savedDisplay.Image != null)
            {

                string folderPath = @"C:\Users\USER PC\Pictures\Camera Roll\Base";


                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }


                string filename = Path.Combine(folderPath, "captured_frame_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".jpeg");


                savedDisplay.Image.Save(filename);
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (savedDisplay.Image != null)
            {

                if (File.Exists(@"C:\Users\USER PC\Pictures\Camera Roll\Base\Base1.jpeg"))
                {
                    Bitmap savedImage = new Bitmap(@"C:\Users\USER PC\Pictures\Camera Roll\Base\Base1.jpeg");


                    if (savedImage.Width == savedDisplay.Image.Width && savedImage.Height == savedDisplay.Image.Height)
                    {

                        Bitmap displayedImage = new Bitmap(savedDisplay.Image);


                        savedImage = ConvertTo24bpp(savedImage);
                        displayedImage = ConvertTo24bpp(displayedImage);


                        ExhaustiveTemplateMatching tm = new ExhaustiveTemplateMatching(0.90f);

                        TemplateMatch[] matchings = tm.ProcessImage(savedImage, displayedImage);


                        if (matchings.Length > 0)
                        {
                            AppendDataToSerialMonitor("Match");
                        }
                        else
                        {
                            AppendDataToSerialMonitor("Different");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Image resolutions do not match.");
                    }
                }
                else
                {
                    MessageBox.Show("Saved image not found.");
                }
            }
        }

        private Bitmap ConvertTo24bpp(Bitmap inputImage)
        {
            if (inputImage.PixelFormat == PixelFormat.Format24bppRgb)
            {

                return inputImage;
            }


            Bitmap newImage = new Bitmap(inputImage.Width, inputImage.Height, PixelFormat.Format24bppRgb);


            using (Graphics g = Graphics.FromImage(newImage))
            {
                g.DrawImage(inputImage, new Rectangle(0, 0, inputImage.Width, inputImage.Height));
            }

            return newImage;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            string folderPath = @"C:\Users\USER PC\Pictures\Camera Roll";
            if (Directory.Exists(folderPath))
            {
                Process.Start("explorer.exe", folderPath);
            }
            else
            {
                MessageBox.Show("The folder doesn't exist or is inaccessible.");
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                openFileDialog.Title = "Select an Image File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using (Bitmap originalImage = new Bitmap(openFileDialog.FileName))
                    {
                        int desiredWidth = 200;
                        int desiredHeight = 200;

                        using (Bitmap croppedImage = new Bitmap(desiredWidth, desiredHeight))
                        {
                            using (Graphics g = Graphics.FromImage(croppedImage))
                            {
                                g.DrawImage(originalImage, new Rectangle(0, 0, desiredWidth, desiredHeight), new Rectangle(50, 50, desiredWidth, desiredHeight), GraphicsUnit.Pixel);
                            }

                            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                            {
                                saveFileDialog.Filter = "Image Files|*.jpg";
                                saveFileDialog.Title = "Save Cropped Image";

                                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                                {
                                    croppedImage.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                                    MessageBox.Show("Cropped image saved successfully!");
                                }
                            }
                        }
                    }
                }
            }
        }
        public async Task<bool> HomeAsync()
        {
            try
            {
                CommandString("h");

                // Use Task.Delay to introduce a timeout
                await Task.Delay(1000);

                return true; // Successfully sent within the timeout
            }
            catch (Exception ex)
            {
                // Handle exceptions, log, or return false if an error occurs
                Console.WriteLine($"Error sending character: {ex.Message}");
                return false;
            }
        }
        private void currentDisplay_Click(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (IsSerialPortOpen())
            {
                string inputText = "a1c";
                string currentTime = DateTime.Now.ToString("HH:mm:ss");
                string formattedData = $"# [{currentTime}] - Check Complete\n";
                
              
                for (int i = 0; i < inputText.Length; i++)
                {
                    char c = inputText[i];
                    serialPort.Write(c.ToString());

                    while (!receivedDataBuffer.Contains("d"))
                    {

                        Application.DoEvents();
                    }


                    receivedDataBuffer = "";


                    if (i == inputText.Length - 1)
                    {

                        await Task.Delay(1000);
                        WebCapture();
                        await HomeAsync();


                        serialMonitorTextBox.AppendText(formattedData);
                        inputBox.Clear();
                    }
                }
            }
            else
            {
                ShowSerialPortErrorMessage();
            }
        }
    }

}

