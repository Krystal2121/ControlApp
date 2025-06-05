using AxWMPLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlApp
{  
    
    public partial class Subliminal : Form
    {
        private System.Windows.Forms.Timer tmr;
        const int GWL_STYLE = (-20);

        const UInt32 WS_POPUP = 0x80000000;
        const UInt32 WS_CHILD = 0x20000000;

        [DllImport("user32.dll", SetLastError = true)]
        static extern UInt32 GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, UInt32 dwNewLong);
        

        public Subliminal(string what,Boolean message)
        {
            InitializeComponent();

            StartPosition = FormStartPosition.CenterScreen;
            Screen[] my = Screen.AllScreens;
            Size = my[0].Bounds.Size;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;  // no borders

            TopMost = true;        // make the form always on top                     
            Visible = true;        // Important! if this isn't set, then the form is not shown at all

            // Set the form click-through
            UInt32 initialStyle = GetWindowLong(this.Handle, -20);
            SetWindowLong(this.Handle, -20, initialStyle | 0x80000 | 0x20);

            tmr = new System.Windows.Forms.Timer();
            tmr.Tick += delegate
            {
                this.Close();
            };
            Random rand = new Random();
            if ((ConfigurationManager.AppSettings["PopSet"] != null) && ("Long" == ConfigurationManager.AppSettings["PopSet"].ToString()))
            {
                int mins = rand.Next(11);
                tmr.Interval = (int)TimeSpan.FromMinutes(mins).TotalMilliseconds;
            }
            else
            {
                int mins = rand.Next(55) + 5;
                tmr.Interval = (int)TimeSpan.FromSeconds(mins).TotalMilliseconds;
            }
            tmr.Start();
            if (message)
            {
                label1.Text = what;
                axWindowsMediaPlayer1.Visible = false;
            }
            else
            {
                label1.Visible = false;
                axWindowsMediaPlayer1.URL = what;
                axWindowsMediaPlayer1.Ctlenabled = false;
                axWindowsMediaPlayer1.uiMode = "None";
                axWindowsMediaPlayer1.settings.autoStart = true;
                axWindowsMediaPlayer1.settings.setMode("loop", true);
            }
        }

        private void Subliminal_Load(object sender, EventArgs e)
        {
                       
        }
    }
}
