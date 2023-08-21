using System;
using Tesseract;
using System.IO.Ports;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace Arduino_Interface
{
    public partial class Form1 : Form
    {
        private string receivedDataBuffer = "";
        private SerialPort serialPort;

        public Form1()
        {
            InitializeComponent();

            string[] availablePorts = SerialPort.GetPortNames();
            comboBoxPort.Items.AddRange(availablePorts);
           
        }

        private bool IsSerialPortOpen()
        {
            return serialPort != null && serialPort.IsOpen;
        }
        private void ShowSerialPortErrorMessage()
        {
            MessageBox.Show("Serial port is not open. Connect to the Arduino first.");
        }
        private void comboBoxPort_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string receivedData = serialPort.ReadExisting();
            receivedDataBuffer += receivedData;

            if (!receivedData.Contains("done"))
            {
                AppendDataToSerialMonitor(receivedData);
            }
        }


        private void AppendDataToSerialMonitor(string data)
        {
            string currentTime = DateTime.Now.ToString("HH:mm:ss");
            string formattedData = $"# [{currentTime}] - {data}\n";

            // Use the Invoke method to safely update the UI control from a different thread
            if (serialMonitorTextBox.InvokeRequired)
            {
                serialMonitorTextBox.Invoke(new Action<string>(AppendDataToSerialMonitor), new object[] { data });
            }
            else
            {
                bool shouldAutoScroll = IsAutoScrollEnabled(serialMonitorTextBox);

                // Append the received data with time and newline to the existing content of the UI control
                serialMonitorTextBox.AppendText(formattedData);

                // Auto scroll to the latest data
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
            serialPort = new SerialPort(selectedPort, 115200); // Set appropriate baud rate and other settings
            try
            {
                serialPort.Open();
                if (serialPort.IsOpen)
                {
                    // Send initial data to the Arduino after the connection is established
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


        private void button5_Click(object sender, EventArgs e)
        {

            if (IsSerialPortOpen())
            {
                serialPort.Write("5");
            }
            else
            {
                ShowSerialPortErrorMessage();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (IsSerialPortOpen())
            {
                serialPort.Write("1");
            }
            else
            {
                ShowSerialPortErrorMessage();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (IsSerialPortOpen())
            {
                serialPort.Write("6");
            }
            else
            {
                ShowSerialPortErrorMessage();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (IsSerialPortOpen())
            {
                serialPort.Write("8");
            }
            else
            {
                ShowSerialPortErrorMessage();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (IsSerialPortOpen())
            {
                serialPort.Write("3");
            }
            else
            {
                ShowSerialPortErrorMessage();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (IsSerialPortOpen())
            {
                serialPort.Write("4");
            }
            else
            {
                ShowSerialPortErrorMessage();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (IsSerialPortOpen())
            {
                serialPort.Write("7");
            }
            else
            {
                ShowSerialPortErrorMessage();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (IsSerialPortOpen())
            {
                serialPort.Write("2");
            }
            else
            {
                ShowSerialPortErrorMessage();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (IsSerialPortOpen())
            {
                serialPort.Write("9");
            }
            else
            {
                ShowSerialPortErrorMessage();
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (IsSerialPortOpen())
            {
                serialPort.Write("0");
            }
            else
            {
                ShowSerialPortErrorMessage();
            }
        }

        private void onButton_Click(object sender, EventArgs e)
        {
            if (IsSerialPortOpen())
            {
                string inputText = "a1c";
                string currentTime = DateTime.Now.ToString("HH:mm:ss");
                string formattedData = $"# [{currentTime}] - {inputText}\n";
                for (int i = 0; i < inputText.Length; i++)
                {
                    char c = inputText[i];
                    serialPort.Write(c.ToString());

                    while (!receivedDataBuffer.Contains("done"))
                    {
                        // Wait and allow the SerialPort_DataReceived event to handle the feedback
                        Application.DoEvents();
                    }

                    // Clear the buffer after receiving feedback
                    receivedDataBuffer = "";

                    // If this is the last character, clear the inputBox
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

        private void Form1_Load(object sender, EventArgs e)
        {

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

                    while (!receivedDataBuffer.Contains("done"))
                    {
                        // Wait and allow the SerialPort_DataReceived event to handle the feedback
                        Application.DoEvents();
                    }

                    // Clear the buffer after receiving feedback
                    receivedDataBuffer = "";

                    // If this is the last character, clear the inputBox
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


    

       

        private void inputBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void serialMonitorTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg;*.gif;*.bmp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp|All Files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string imagePath = openFileDialog.FileName;

                // Perform OCR
                string extractedText = PerformOCR(imagePath);

                // Display the extracted text in the Label
                label1.Text = extractedText;
            }
        }

        private string PerformOCR(string imagePath)
        {
            using (var engine = new TesseractEngine("./tessdata", "seg", EngineMode.TesseractAndLstm))
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
    }
}

