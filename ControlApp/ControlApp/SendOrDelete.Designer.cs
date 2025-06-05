namespace ControlApp
{
    partial class SendOrDelete
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SendOrDelete));
            axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            Sendbtn = new Button();
            Deletebtn = new Button();
            ((System.ComponentModel.ISupportInitialize)axWindowsMediaPlayer1).BeginInit();
            SuspendLayout();
            // 
            // axWindowsMediaPlayer1
            // 
            axWindowsMediaPlayer1.Dock = DockStyle.Top;
            axWindowsMediaPlayer1.Enabled = true;
            axWindowsMediaPlayer1.Location = new Point(0, 0);
            axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            axWindowsMediaPlayer1.OcxState = (AxHost.State)resources.GetObject("axWindowsMediaPlayer1.OcxState");
            axWindowsMediaPlayer1.Size = new Size(800, 450);
            axWindowsMediaPlayer1.TabIndex = 1;
            // 
            // Sendbtn
            // 
            Sendbtn.Location = new Point(279, 468);
            Sendbtn.Name = "Sendbtn";
            Sendbtn.Size = new Size(75, 23);
            Sendbtn.TabIndex = 2;
            Sendbtn.Text = "Send";
            Sendbtn.UseVisualStyleBackColor = true;
            Sendbtn.Click += Sendbtn_Click;
            // 
            // Deletebtn
            // 
            Deletebtn.Location = new Point(384, 468);
            Deletebtn.Name = "Deletebtn";
            Deletebtn.Size = new Size(75, 23);
            Deletebtn.TabIndex = 3;
            Deletebtn.Text = "Delete";
            Deletebtn.UseVisualStyleBackColor = true;
            Deletebtn.Click += Deletebtn_Click;
            // 
            // SendOrDelete
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 503);
            ControlBox = false;
            Controls.Add(Deletebtn);
            Controls.Add(Sendbtn);
            Controls.Add(axWindowsMediaPlayer1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "SendOrDelete";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "SendOrDelete";
            TopMost = true;
            Load += SendOrDelete_Load;
            ((System.ComponentModel.ISupportInitialize)axWindowsMediaPlayer1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
        private Button Sendbtn;
        private Button Deletebtn;
    }
}