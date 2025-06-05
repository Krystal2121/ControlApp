namespace ControlApp
{
    partial class Options
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
            SvExit = new Button();
            tabPage1 = new TabPage();
            panel10 = new Panel();
            fullscchk = new CheckBox();
            panel6 = new Panel();
            ymove = new RadioButton();
            nmove = new RadioButton();
            panel5 = new Panel();
            seethr = new RadioButton();
            nseethr = new RadioButton();
            panel11 = new Panel();
            clkbl = new RadioButton();
            clickthr = new RadioButton();
            normlbtn = new RadioButton();
            label11 = new Label();
            panel4 = new Panel();
            dmode_chk = new CheckBox();
            webcnt = new CheckBox();
            label5 = new Label();
            showblocked = new CheckBox();
            panel3 = new Panel();
            parallelrd = new RadioButton();
            serialrd = new RadioButton();
            label4 = new Label();
            panel2 = new Panel();
            fitsc = new RadioButton();
            stret = new RadioButton();
            label3 = new Label();
            panel1 = new Panel();
            dismch = new CheckBox();
            ttsch = new CheckBox();
            webcamch = new CheckBox();
            outstandch = new CheckBox();
            senddelch = new CheckBox();
            watch4mech = new CheckBox();
            twitch = new CheckBox();
            screenshch = new CheckBox();
            wrich = new CheckBox();
            audioch = new CheckBox();
            sublimch = new CheckBox();
            messch = new CheckBox();
            popch = new CheckBox();
            label2 = new Label();
            opwebch = new CheckBox();
            runch = new CheckBox();
            wallpch = new CheckBox();
            dlch = new CheckBox();
            Popups = new Panel();
            longpop = new RadioButton();
            shortpop = new RadioButton();
            label1 = new Label();
            tabControl1 = new TabControl();
            tabPage1.SuspendLayout();
            panel10.SuspendLayout();
            panel6.SuspendLayout();
            panel5.SuspendLayout();
            panel11.SuspendLayout();
            panel4.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            Popups.SuspendLayout();
            tabControl1.SuspendLayout();
            SuspendLayout();
            // 
            // SvExit
            // 
            SvExit.Location = new Point(319, 477);
            SvExit.Name = "SvExit";
            SvExit.Size = new Size(81, 23);
            SvExit.TabIndex = 1;
            SvExit.Text = "Save & Exit ";
            SvExit.UseVisualStyleBackColor = true;
            SvExit.Click += SvExit_Click;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(panel10);
            tabPage1.Controls.Add(panel4);
            tabPage1.Controls.Add(panel3);
            tabPage1.Controls.Add(panel2);
            tabPage1.Controls.Add(panel1);
            tabPage1.Controls.Add(Popups);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(685, 431);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "General";
            tabPage1.UseVisualStyleBackColor = true;
            tabPage1.Click += tabPage1_Click;
            // 
            // panel10
            // 
            panel10.Controls.Add(fullscchk);
            panel10.Controls.Add(panel6);
            panel10.Controls.Add(panel5);
            panel10.Controls.Add(panel11);
            panel10.Controls.Add(label11);
            panel10.Location = new Point(346, 91);
            panel10.Name = "panel10";
            panel10.Size = new Size(334, 208);
            panel10.TabIndex = 30;
            // 
            // fullscchk
            // 
            fullscchk.AutoSize = true;
            fullscchk.Location = new Point(159, 108);
            fullscchk.Name = "fullscchk";
            fullscchk.Size = new Size(83, 19);
            fullscchk.TabIndex = 10;
            fullscchk.Text = "Full Screen";
            fullscchk.UseVisualStyleBackColor = true;
            fullscchk.CheckedChanged += fullscchk_CheckedChanged;
            // 
            // panel6
            // 
            panel6.Controls.Add(ymove);
            panel6.Controls.Add(nmove);
            panel6.Location = new Point(146, 26);
            panel6.Name = "panel6";
            panel6.Size = new Size(119, 62);
            panel6.TabIndex = 9;
            // 
            // ymove
            // 
            ymove.AutoSize = true;
            ymove.Location = new Point(13, 32);
            ymove.Name = "ymove";
            ymove.Size = new Size(66, 19);
            ymove.TabIndex = 1;
            ymove.TabStop = true;
            ymove.Text = "Moving";
            ymove.UseVisualStyleBackColor = true;
            // 
            // nmove
            // 
            nmove.AutoSize = true;
            nmove.Location = new Point(13, 7);
            nmove.Name = "nmove";
            nmove.Size = new Size(72, 19);
            nmove.TabIndex = 6;
            nmove.TabStop = true;
            nmove.Text = "Standard";
            nmove.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            panel5.Controls.Add(seethr);
            panel5.Controls.Add(nseethr);
            panel5.Location = new Point(21, 115);
            panel5.Name = "panel5";
            panel5.Size = new Size(119, 62);
            panel5.TabIndex = 8;
            // 
            // seethr
            // 
            seethr.AutoSize = true;
            seethr.Location = new Point(13, 32);
            seethr.Name = "seethr";
            seethr.Size = new Size(89, 19);
            seethr.TabIndex = 1;
            seethr.TabStop = true;
            seethr.Text = "See through";
            seethr.UseVisualStyleBackColor = true;
            // 
            // nseethr
            // 
            nseethr.AutoSize = true;
            nseethr.Location = new Point(13, 7);
            nseethr.Name = "nseethr";
            nseethr.Size = new Size(72, 19);
            nseethr.TabIndex = 6;
            nseethr.TabStop = true;
            nseethr.Text = "Standard";
            nseethr.UseVisualStyleBackColor = true;
            // 
            // panel11
            // 
            panel11.Controls.Add(clkbl);
            panel11.Controls.Add(clickthr);
            panel11.Controls.Add(normlbtn);
            panel11.Location = new Point(21, 26);
            panel11.Name = "panel11";
            panel11.Size = new Size(119, 87);
            panel11.TabIndex = 7;
            // 
            // clkbl
            // 
            clkbl.AutoSize = true;
            clkbl.Location = new Point(13, 56);
            clkbl.Name = "clkbl";
            clkbl.Size = new Size(73, 19);
            clkbl.TabIndex = 7;
            clkbl.TabStop = true;
            clkbl.Text = "Clickable";
            clkbl.UseVisualStyleBackColor = true;
            // 
            // clickthr
            // 
            clickthr.AutoSize = true;
            clickthr.Location = new Point(13, 32);
            clickthr.Name = "clickthr";
            clickthr.Size = new Size(97, 19);
            clickthr.TabIndex = 1;
            clickthr.TabStop = true;
            clickthr.Text = "Click through";
            clickthr.UseVisualStyleBackColor = true;
            // 
            // normlbtn
            // 
            normlbtn.AutoSize = true;
            normlbtn.Location = new Point(13, 7);
            normlbtn.Name = "normlbtn";
            normlbtn.Size = new Size(72, 19);
            normlbtn.TabIndex = 6;
            normlbtn.TabStop = true;
            normlbtn.Text = "Standard";
            normlbtn.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(3, 0);
            label11.Name = "label11";
            label11.Size = new Size(74, 15);
            label11.TabIndex = 0;
            label11.Text = "Popups Type";
            // 
            // panel4
            // 
            panel4.Controls.Add(dmode_chk);
            panel4.Controls.Add(webcnt);
            panel4.Controls.Add(label5);
            panel4.Controls.Add(showblocked);
            panel4.Location = new Point(346, 305);
            panel4.Name = "panel4";
            panel4.Size = new Size(334, 114);
            panel4.TabIndex = 29;
            // 
            // dmode_chk
            // 
            dmode_chk.AutoSize = true;
            dmode_chk.Location = new Point(21, 68);
            dmode_chk.Name = "dmode_chk";
            dmode_chk.Size = new Size(84, 19);
            dmode_chk.TabIndex = 23;
            dmode_chk.Text = "Dark mode";
            dmode_chk.UseVisualStyleBackColor = true;
            // 
            // webcnt
            // 
            webcnt.AutoSize = true;
            webcnt.Location = new Point(21, 18);
            webcnt.Name = "webcnt";
            webcnt.Size = new Size(137, 19);
            webcnt.TabIndex = 22;
            webcnt.Text = "Webcam countdown";
            webcnt.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(7, 0);
            label5.Name = "label5";
            label5.Size = new Size(49, 15);
            label5.TabIndex = 21;
            label5.Text = "Options";
            // 
            // showblocked
            // 
            showblocked.AutoSize = true;
            showblocked.Location = new Point(21, 43);
            showblocked.Name = "showblocked";
            showblocked.Size = new Size(126, 19);
            showblocked.TabIndex = 20;
            showblocked.Text = "Show blocked msg";
            showblocked.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            panel3.Controls.Add(parallelrd);
            panel3.Controls.Add(serialrd);
            panel3.Controls.Add(label4);
            panel3.Location = new Point(346, 6);
            panel3.Name = "panel3";
            panel3.Size = new Size(334, 77);
            panel3.TabIndex = 27;
            // 
            // parallelrd
            // 
            parallelrd.AutoSize = true;
            parallelrd.Location = new Point(21, 43);
            parallelrd.Name = "parallelrd";
            parallelrd.Size = new Size(198, 19);
            parallelrd.TabIndex = 2;
            parallelrd.TabStop = true;
            parallelrd.Text = "All at once (may cause pc issues)";
            parallelrd.UseVisualStyleBackColor = true;
            parallelrd.CheckedChanged += parallelrd_CheckedChanged;
            // 
            // serialrd
            // 
            serialrd.AutoSize = true;
            serialrd.Location = new Point(21, 18);
            serialrd.Name = "serialrd";
            serialrd.Size = new Size(96, 19);
            serialrd.TabIndex = 1;
            serialrd.TabStop = true;
            serialrd.Text = "One at a time";
            serialrd.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(3, 0);
            label4.Name = "label4";
            label4.Size = new Size(47, 15);
            label4.TabIndex = 0;
            label4.Text = "Popups";
            // 
            // panel2
            // 
            panel2.Controls.Add(fitsc);
            panel2.Controls.Add(stret);
            panel2.Controls.Add(label3);
            panel2.Location = new Point(6, 371);
            panel2.Name = "panel2";
            panel2.Size = new Size(334, 48);
            panel2.TabIndex = 28;
            // 
            // fitsc
            // 
            fitsc.AutoSize = true;
            fitsc.Location = new Point(111, 18);
            fitsc.Name = "fitsc";
            fitsc.Size = new Size(75, 19);
            fitsc.TabIndex = 2;
            fitsc.TabStop = true;
            fitsc.Text = "Fit screen";
            fitsc.UseVisualStyleBackColor = true;
            // 
            // stret
            // 
            stret.AutoSize = true;
            stret.Location = new Point(12, 18);
            stret.Name = "stret";
            stret.Size = new Size(62, 19);
            stret.TabIndex = 1;
            stret.TabStop = true;
            stret.Text = "Stretch";
            stret.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 0);
            label3.Name = "label3";
            label3.Size = new Size(60, 15);
            label3.TabIndex = 0;
            label3.Text = "Wallpaper";
            // 
            // panel1
            // 
            panel1.Controls.Add(dismch);
            panel1.Controls.Add(ttsch);
            panel1.Controls.Add(webcamch);
            panel1.Controls.Add(outstandch);
            panel1.Controls.Add(senddelch);
            panel1.Controls.Add(watch4mech);
            panel1.Controls.Add(twitch);
            panel1.Controls.Add(screenshch);
            panel1.Controls.Add(wrich);
            panel1.Controls.Add(audioch);
            panel1.Controls.Add(sublimch);
            panel1.Controls.Add(messch);
            panel1.Controls.Add(popch);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(opwebch);
            panel1.Controls.Add(runch);
            panel1.Controls.Add(wallpch);
            panel1.Controls.Add(dlch);
            panel1.Location = new Point(6, 89);
            panel1.Name = "panel1";
            panel1.Size = new Size(334, 276);
            panel1.TabIndex = 26;
            // 
            // dismch
            // 
            dismch.AutoSize = true;
            dismch.Location = new Point(122, 185);
            dismch.Name = "dismch";
            dismch.Size = new Size(103, 19);
            dismch.TabIndex = 17;
            dismch.Text = "Disable Mouse";
            dismch.UseVisualStyleBackColor = true;
            // 
            // ttsch
            // 
            ttsch.AutoSize = true;
            ttsch.Location = new Point(122, 160);
            ttsch.Name = "ttsch";
            ttsch.Size = new Size(44, 19);
            ttsch.TabIndex = 16;
            ttsch.Text = "TTS";
            ttsch.UseVisualStyleBackColor = true;
            // 
            // webcamch
            // 
            webcamch.AutoSize = true;
            webcamch.Location = new Point(122, 135);
            webcamch.Name = "webcamch";
            webcamch.Size = new Size(73, 19);
            webcamch.TabIndex = 15;
            webcamch.Text = "Webcam";
            webcamch.UseVisualStyleBackColor = true;
            webcamch.CheckedChanged += webcamch_CheckedChanged;
            // 
            // outstandch
            // 
            outstandch.AutoSize = true;
            outstandch.Location = new Point(122, 234);
            outstandch.Name = "outstandch";
            outstandch.Size = new Size(143, 19);
            outstandch.TabIndex = 14;
            outstandch.Text = "Outstanding reminder";
            outstandch.UseVisualStyleBackColor = true;
            // 
            // senddelch
            // 
            senddelch.AutoSize = true;
            senddelch.Location = new Point(122, 110);
            senddelch.Name = "senddelch";
            senddelch.Size = new Size(102, 19);
            senddelch.TabIndex = 13;
            senddelch.Text = "Send or Delete";
            senddelch.UseVisualStyleBackColor = true;
            // 
            // watch4mech
            // 
            watch4mech.AutoSize = true;
            watch4mech.Location = new Point(122, 85);
            watch4mech.Name = "watch4mech";
            watch4mech.Size = new Size(98, 19);
            watch4mech.TabIndex = 12;
            watch4mech.Text = "Watch for me";
            watch4mech.UseVisualStyleBackColor = true;
            // 
            // twitch
            // 
            twitch.AutoSize = true;
            twitch.Location = new Point(122, 60);
            twitch.Name = "twitch";
            twitch.Size = new Size(87, 19);
            twitch.TabIndex = 11;
            twitch.Text = "Twitter post";
            twitch.UseVisualStyleBackColor = true;
            // 
            // screenshch
            // 
            screenshch.AutoSize = true;
            screenshch.Location = new Point(122, 35);
            screenshch.Name = "screenshch";
            screenshch.Size = new Size(92, 19);
            screenshch.TabIndex = 10;
            screenshch.Text = "Screen shots";
            screenshch.UseVisualStyleBackColor = true;
            // 
            // wrich
            // 
            wrich.AutoSize = true;
            wrich.Location = new Point(12, 234);
            wrich.Name = "wrich";
            wrich.Size = new Size(92, 19);
            wrich.TabIndex = 9;
            wrich.Text = "Write for me";
            wrich.UseVisualStyleBackColor = true;
            // 
            // audioch
            // 
            audioch.AutoSize = true;
            audioch.Location = new Point(12, 209);
            audioch.Name = "audioch";
            audioch.Size = new Size(58, 19);
            audioch.TabIndex = 8;
            audioch.Text = "Audio";
            audioch.UseVisualStyleBackColor = true;
            // 
            // sublimch
            // 
            sublimch.AutoSize = true;
            sublimch.Location = new Point(12, 185);
            sublimch.Name = "sublimch";
            sublimch.Size = new Size(82, 19);
            sublimch.TabIndex = 7;
            sublimch.Text = "Subliminal";
            sublimch.UseVisualStyleBackColor = true;
            // 
            // messch
            // 
            messch.AutoSize = true;
            messch.Location = new Point(12, 160);
            messch.Name = "messch";
            messch.Size = new Size(77, 19);
            messch.TabIndex = 6;
            messch.Text = "Messages";
            messch.UseVisualStyleBackColor = true;
            // 
            // popch
            // 
            popch.AutoSize = true;
            popch.Location = new Point(12, 135);
            popch.Name = "popch";
            popch.Size = new Size(70, 19);
            popch.TabIndex = 5;
            popch.Text = "Pop Ups";
            popch.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 0);
            label2.Name = "label2";
            label2.Size = new Size(56, 15);
            label2.TabIndex = 4;
            label2.Text = "Dis-allow";
            // 
            // opwebch
            // 
            opwebch.AutoSize = true;
            opwebch.Location = new Point(12, 110);
            opwebch.Name = "opwebch";
            opwebch.Size = new Size(100, 19);
            opwebch.TabIndex = 3;
            opwebch.Text = "Open Website";
            opwebch.UseVisualStyleBackColor = true;
            // 
            // runch
            // 
            runch.AutoSize = true;
            runch.Location = new Point(12, 85);
            runch.Name = "runch";
            runch.Size = new Size(93, 19);
            runch.TabIndex = 2;
            runch.Text = "Runable files";
            runch.UseVisualStyleBackColor = true;
            // 
            // wallpch
            // 
            wallpch.AutoSize = true;
            wallpch.Location = new Point(12, 60);
            wallpch.Name = "wallpch";
            wallpch.Size = new Size(79, 19);
            wallpch.TabIndex = 1;
            wallpch.Text = "Wallpaper";
            wallpch.UseVisualStyleBackColor = true;
            // 
            // dlch
            // 
            dlch.AutoSize = true;
            dlch.Location = new Point(12, 35);
            dlch.Name = "dlch";
            dlch.Size = new Size(80, 19);
            dlch.TabIndex = 0;
            dlch.Text = "Download";
            dlch.UseVisualStyleBackColor = true;
            // 
            // Popups
            // 
            Popups.Controls.Add(longpop);
            Popups.Controls.Add(shortpop);
            Popups.Controls.Add(label1);
            Popups.Location = new Point(6, 6);
            Popups.Name = "Popups";
            Popups.Size = new Size(334, 77);
            Popups.TabIndex = 25;
            // 
            // longpop
            // 
            longpop.AutoSize = true;
            longpop.Location = new Point(12, 43);
            longpop.Name = "longpop";
            longpop.Size = new Size(185, 19);
            longpop.TabIndex = 2;
            longpop.TabStop = true;
            longpop.Text = "Long popups (1min - 10 mins)";
            longpop.UseVisualStyleBackColor = true;
            // 
            // shortpop
            // 
            shortpop.AutoSize = true;
            shortpop.Location = new Point(12, 18);
            shortpop.Name = "shortpop";
            shortpop.Size = new Size(174, 19);
            shortpop.TabIndex = 1;
            shortpop.TabStop = true;
            shortpop.Text = "Short popups (10sec - 1min)";
            shortpop.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(47, 15);
            label1.TabIndex = 0;
            label1.Text = "Popups";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Location = new Point(12, 12);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(693, 459);
            tabControl1.TabIndex = 20;
            // 
            // Options
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(709, 509);
            Controls.Add(tabControl1);
            Controls.Add(SvExit);
            Name = "Options";
            Text = "Options";
            Load += Options_Load;
            tabPage1.ResumeLayout(false);
            panel10.ResumeLayout(false);
            panel10.PerformLayout();
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panel11.ResumeLayout(false);
            panel11.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            Popups.ResumeLayout(false);
            Popups.PerformLayout();
            tabControl1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Button SvExit;
        private TabPage tabPage1;
        private Panel panel10;
        private Panel panel6;
        private RadioButton ymove;
        private RadioButton nmove;
        private Panel panel5;
        private RadioButton seethr;
        private RadioButton nseethr;
        private Panel panel11;
        private RadioButton clkbl;
        private RadioButton clickthr;
        private RadioButton normlbtn;
        private Label label11;
        private Panel panel4;
        private CheckBox webcnt;
        private Label label5;
        private CheckBox showblocked;
        private Panel panel3;
        private RadioButton parallelrd;
        private RadioButton serialrd;
        private Label label4;
        private Panel panel2;
        private RadioButton fitsc;
        private RadioButton stret;
        private Label label3;
        private Panel panel1;
        private CheckBox dismch;
        private CheckBox ttsch;
        private CheckBox webcamch;
        private CheckBox outstandch;
        private CheckBox senddelch;
        private CheckBox watch4mech;
        private CheckBox twitch;
        private CheckBox screenshch;
        private CheckBox wrich;
        private CheckBox audioch;
        private CheckBox sublimch;
        private CheckBox messch;
        private CheckBox popch;
        private Label label2;
        private CheckBox opwebch;
        private CheckBox runch;
        private CheckBox wallpch;
        private CheckBox dlch;
        private Panel Popups;
        private RadioButton longpop;
        private RadioButton shortpop;
        private Label label1;
        private TabControl tabControl1;
        private CheckBox fullscchk;
        private CheckBox dmode_chk;
    }
}