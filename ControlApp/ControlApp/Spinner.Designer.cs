namespace ControlApp
{
    partial class Spinner
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
            pictureBox1 = new PictureBox();
            SpinBtn = new Button();
            label1 = new Label();
            resultbox_txt = new TextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(12, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(391, 372);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // SpinBtn
            // 
            SpinBtn.Location = new Point(328, 390);
            SpinBtn.Name = "SpinBtn";
            SpinBtn.Size = new Size(75, 23);
            SpinBtn.TabIndex = 1;
            SpinBtn.Text = "Spin";
            SpinBtn.UseVisualStyleBackColor = true;
            SpinBtn.Click += SpinBtn_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(7, 393);
            label1.Name = "label1";
            label1.Size = new Size(45, 15);
            label1.TabIndex = 2;
            label1.Text = "Result :";
            // 
            // resultbox_txt
            // 
            resultbox_txt.Location = new Point(58, 390);
            resultbox_txt.Name = "resultbox_txt";
            resultbox_txt.Size = new Size(260, 23);
            resultbox_txt.TabIndex = 3;
            // 
            // Spinner
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(419, 430);
            Controls.Add(resultbox_txt);
            Controls.Add(label1);
            Controls.Add(SpinBtn);
            Controls.Add(pictureBox1);
            Name = "Spinner";
            Text = "Spinner";
            Load += Spinner_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Button SpinBtn;
        private Label label1;
        private TextBox resultbox_txt;
    }
}