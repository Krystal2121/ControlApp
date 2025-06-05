namespace ControlApp
{
    partial class WriteForMe
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
            components = new System.ComponentModel.Container();
            button1 = new Button();
            label1 = new Label();
            writelbl = new Label();
            wfmtextb = new TextBox();
            timelbl = new Label();
            label3 = new Label();
            label4 = new Label();
            mistakelbl = new Label();
            label6 = new Label();
            countlbl = new Label();
            timer1 = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(398, 136);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 0;
            button1.Text = "Fail";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            label1.Location = new Point(21, 19);
            label1.Name = "label1";
            label1.Size = new Size(125, 25);
            label1.TabIndex = 1;
            label1.Text = "Write for me";
            // 
            // writelbl
            // 
            writelbl.AutoSize = true;
            writelbl.Location = new Point(21, 59);
            writelbl.Name = "writelbl";
            writelbl.Size = new Size(38, 15);
            writelbl.TabIndex = 2;
            writelbl.Text = "label2";
            // 
            // wfmtextb
            // 
            wfmtextb.Location = new Point(21, 95);
            wfmtextb.Name = "wfmtextb";
            wfmtextb.Size = new Size(452, 23);
            wfmtextb.TabIndex = 3;
            wfmtextb.KeyDown += input_KeyDown;
            // 
            // timelbl
            // 
            timelbl.AutoSize = true;
            timelbl.Location = new Point(420, 19);
            timelbl.Name = "timelbl";
            timelbl.Size = new Size(38, 15);
            timelbl.TabIndex = 4;
            timelbl.Text = "label2";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(366, 19);
            label3.Name = "label3";
            label3.Size = new Size(33, 15);
            label3.TabIndex = 5;
            label3.Text = "Time";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(366, 34);
            label4.Name = "label4";
            label4.Size = new Size(53, 15);
            label4.TabIndex = 6;
            label4.Text = "Mistakes";
            // 
            // mistakelbl
            // 
            mistakelbl.AutoSize = true;
            mistakelbl.Location = new Point(420, 34);
            mistakelbl.Name = "mistakelbl";
            mistakelbl.Size = new Size(38, 15);
            mistakelbl.TabIndex = 7;
            mistakelbl.Text = "label5";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(366, 49);
            label6.Name = "label6";
            label6.Size = new Size(27, 15);
            label6.TabIndex = 8;
            label6.Text = "Left";
            // 
            // countlbl
            // 
            countlbl.AutoSize = true;
            countlbl.Location = new Point(420, 49);
            countlbl.Name = "countlbl";
            countlbl.Size = new Size(38, 15);
            countlbl.TabIndex = 9;
            countlbl.Text = "label7";
            // 
            // WriteForMe
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(492, 173);
            ControlBox = false;
            Controls.Add(countlbl);
            Controls.Add(label6);
            Controls.Add(mistakelbl);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(timelbl);
            Controls.Add(wfmtextb);
            Controls.Add(writelbl);
            Controls.Add(label1);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "WriteForMe";
            ShowIcon = false;
            ShowInTaskbar = false;
            Text = "WriteForMe";
            TopMost = true;
            Load += WriteForMe_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Label label1;
        private Label writelbl;
        private TextBox wfmtextb;
        private Label timelbl;
        private Label label3;
        private Label label4;
        private Label mistakelbl;
        private Label label6;
        private Label countlbl;
        private System.Windows.Forms.Timer timer1;
    }
}