namespace Arduino_Interface
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.comboBoxPort = new System.Windows.Forms.ComboBox();
            this.online = new System.Windows.Forms.Button();
            this.offline = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.serialMonitorTextBox = new System.Windows.Forms.RichTextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.inputBox = new System.Windows.Forms.RichTextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.crop = new System.Windows.Forms.Button();
            this.folderSave = new System.Windows.Forms.Button();
            this.compare = new System.Windows.Forms.Button();
            this.save = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.captureImage = new System.Windows.Forms.Button();
            this.home = new System.Windows.Forms.Button();
            this.checknumber = new System.Windows.Forms.Button();
            this.currentDisplay = new System.Windows.Forms.PictureBox();
            this.savedDisplay = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.currentDisplay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.savedDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBoxPort
            // 
            this.comboBoxPort.FormattingEnabled = true;
            this.comboBoxPort.ItemHeight = 16;
            this.comboBoxPort.Location = new System.Drawing.Point(29, 31);
            this.comboBoxPort.Name = "comboBoxPort";
            this.comboBoxPort.Size = new System.Drawing.Size(214, 24);
            this.comboBoxPort.TabIndex = 0;
            this.comboBoxPort.Text = "Choose Serial Port";
            // 
            // online
            // 
            this.online.BackColor = System.Drawing.SystemColors.Highlight;
            this.online.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.online.Location = new System.Drawing.Point(29, 70);
            this.online.Name = "online";
            this.online.Size = new System.Drawing.Size(100, 50);
            this.online.TabIndex = 1;
            this.online.Text = "Connect";
            this.online.UseVisualStyleBackColor = false;
            this.online.Click += new System.EventHandler(this.online_Click);
            // 
            // offline
            // 
            this.offline.BackColor = System.Drawing.Color.IndianRed;
            this.offline.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.offline.Location = new System.Drawing.Point(143, 70);
            this.offline.Name = "offline";
            this.offline.Size = new System.Drawing.Size(100, 50);
            this.offline.TabIndex = 2;
            this.offline.Text = "Disconnect";
            this.offline.UseVisualStyleBackColor = false;
            this.offline.Click += new System.EventHandler(this.offline_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBoxPort);
            this.groupBox2.Controls.Add(this.offline);
            this.groupBox2.Controls.Add(this.online);
            this.groupBox2.Location = new System.Drawing.Point(12, 22);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(271, 151);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Serial Connection";
            // 
            // sendButton
            // 
            this.sendButton.BackColor = System.Drawing.SystemColors.MenuHighlight;
            this.sendButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.sendButton.ForeColor = System.Drawing.SystemColors.Control;
            this.sendButton.Location = new System.Drawing.Point(85, 94);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(100, 40);
            this.sendButton.TabIndex = 6;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = false;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // serialMonitorTextBox
            // 
            this.serialMonitorTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.serialMonitorTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.serialMonitorTextBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.serialMonitorTextBox.Location = new System.Drawing.Point(6, 21);
            this.serialMonitorTextBox.Name = "serialMonitorTextBox";
            this.serialMonitorTextBox.ReadOnly = true;
            this.serialMonitorTextBox.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.serialMonitorTextBox.Size = new System.Drawing.Size(224, 492);
            this.serialMonitorTextBox.TabIndex = 7;
            this.serialMonitorTextBox.TabStop = false;
            this.serialMonitorTextBox.Text = "";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.inputBox);
            this.groupBox3.Controls.Add(this.sendButton);
            this.groupBox3.Location = new System.Drawing.Point(12, 179);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(271, 140);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Pin Input";
            // 
            // inputBox
            // 
            this.inputBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inputBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputBox.Location = new System.Drawing.Point(29, 21);
            this.inputBox.Multiline = false;
            this.inputBox.Name = "inputBox";
            this.inputBox.Size = new System.Drawing.Size(214, 68);
            this.inputBox.TabIndex = 9;
            this.inputBox.Text = "";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.serialMonitorTextBox);
            this.groupBox4.Location = new System.Drawing.Point(289, 22);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(236, 519);
            this.groupBox4.TabIndex = 9;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Serial Monitor";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.button2);
            this.groupBox5.Controls.Add(this.button1);
            this.groupBox5.Controls.Add(this.crop);
            this.groupBox5.Controls.Add(this.folderSave);
            this.groupBox5.Controls.Add(this.compare);
            this.groupBox5.Controls.Add(this.save);
            this.groupBox5.Controls.Add(this.button13);
            this.groupBox5.Controls.Add(this.captureImage);
            this.groupBox5.Controls.Add(this.home);
            this.groupBox5.Controls.Add(this.checknumber);
            this.groupBox5.Location = new System.Drawing.Point(12, 325);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(271, 216);
            this.groupBox5.TabIndex = 10;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Command";
            // 
            // crop
            // 
            this.crop.Location = new System.Drawing.Point(142, 94);
            this.crop.Name = "crop";
            this.crop.Size = new System.Drawing.Size(123, 29);
            this.crop.TabIndex = 16;
            this.crop.Text = "Crop";
            this.crop.UseVisualStyleBackColor = true;
            this.crop.Click += new System.EventHandler(this.button18_Click);
            // 
            // folderSave
            // 
            this.folderSave.Location = new System.Drawing.Point(142, 58);
            this.folderSave.Name = "folderSave";
            this.folderSave.Size = new System.Drawing.Size(123, 29);
            this.folderSave.TabIndex = 15;
            this.folderSave.Text = "Saved Folder";
            this.folderSave.UseVisualStyleBackColor = true;
            this.folderSave.Click += new System.EventHandler(this.button17_Click);
            // 
            // compare
            // 
            this.compare.Location = new System.Drawing.Point(142, 22);
            this.compare.Name = "compare";
            this.compare.Size = new System.Drawing.Size(123, 29);
            this.compare.TabIndex = 14;
            this.compare.Text = "Compare";
            this.compare.UseVisualStyleBackColor = true;
            this.compare.Click += new System.EventHandler(this.button16_Click);
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(142, 164);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(123, 29);
            this.save.TabIndex = 13;
            this.save.Text = "Save Base";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.button15_Click);
            // 
            // button13
            // 
            this.button13.Location = new System.Drawing.Point(6, 93);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(123, 30);
            this.button13.TabIndex = 2;
            this.button13.Text = "Command 1";
            this.button13.UseVisualStyleBackColor = true;
            this.button13.Click += new System.EventHandler(this.button13_Click);
            // 
            // captureImage
            // 
            this.captureImage.Location = new System.Drawing.Point(142, 129);
            this.captureImage.Name = "captureImage";
            this.captureImage.Size = new System.Drawing.Size(123, 29);
            this.captureImage.TabIndex = 12;
            this.captureImage.Text = "Capture Image";
            this.captureImage.UseVisualStyleBackColor = true;
            this.captureImage.Click += new System.EventHandler(this.button14_Click);
            // 
            // home
            // 
            this.home.Location = new System.Drawing.Point(6, 57);
            this.home.Name = "home";
            this.home.Size = new System.Drawing.Size(123, 30);
            this.home.TabIndex = 1;
            this.home.Text = "ON_Pass";
            this.home.UseVisualStyleBackColor = true;
            this.home.Click += new System.EventHandler(this.button12_Click);
            // 
            // checknumber
            // 
            this.checknumber.Location = new System.Drawing.Point(6, 21);
            this.checknumber.Name = "checknumber";
            this.checknumber.Size = new System.Drawing.Size(123, 30);
            this.checknumber.TabIndex = 0;
            this.checknumber.Text = "Check Number";
            this.checknumber.UseVisualStyleBackColor = true;
            this.checknumber.Click += new System.EventHandler(this.button11_Click);
            // 
            // currentDisplay
            // 
            this.currentDisplay.Location = new System.Drawing.Point(531, 28);
            this.currentDisplay.Name = "currentDisplay";
            this.currentDisplay.Size = new System.Drawing.Size(320, 240);
            this.currentDisplay.TabIndex = 11;
            this.currentDisplay.TabStop = false;
            this.currentDisplay.Click += new System.EventHandler(this.currentDisplay_Click);
            // 
            // savedDisplay
            // 
            this.savedDisplay.Location = new System.Drawing.Point(531, 301);
            this.savedDisplay.Name = "savedDisplay";
            this.savedDisplay.Size = new System.Drawing.Size(320, 240);
            this.savedDisplay.TabIndex = 13;
            this.savedDisplay.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 129);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(123, 30);
            this.button1.TabIndex = 17;
            this.button1.Text = "Command 2";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 165);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(123, 30);
            this.button2.TabIndex = 18;
            this.button2.Text = "Command 3";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(859, 574);
            this.Controls.Add(this.savedDisplay);
            this.Controls.Add(this.currentDisplay);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Name = "Form1";
            this.Text = "Arduino Interface";
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.currentDisplay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.savedDisplay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxPort;
        private System.Windows.Forms.Button online;
        private System.Windows.Forms.Button offline;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RichTextBox inputBox;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RichTextBox serialMonitorTextBox;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button button13;
        private System.Windows.Forms.Button home;
        private System.Windows.Forms.Button checknumber;
        private System.Windows.Forms.Button captureImage;
        private System.Windows.Forms.PictureBox currentDisplay;
        private System.Windows.Forms.PictureBox savedDisplay;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.Button compare;
        private System.Windows.Forms.Button folderSave;
        private System.Windows.Forms.Button crop;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
    }
}

