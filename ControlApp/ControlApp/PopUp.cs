using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlApp
{
    public partial class PopUp : Form
    {
        public string run_url;
        private System.Windows.Forms.Timer tmr;

        const int GWL_STYLE = (-20);

        const UInt32 WS_POPUP = 0x80000000;
        const UInt32 WS_CHILD = 0x20000000;

        [DllImport("user32.dll", SetLastError = true)]
        static extern UInt32 GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, UInt32 dwNewLong);

        List<string> urls;
        char[] poptypearr;
        Utils Utils;
        public PopUp(string url)
        {
            Utils = new Utils();
            urls = new List<string>();
            InitializeComponent();
            string poptypestr = "nnnn";
            if (ConfigurationManager.AppSettings["PopType"] != null)
            {
                poptypestr = ConfigurationManager.AppSettings["PopType"];
            }
            poptypearr = poptypestr.ToCharArray();

            if (poptypearr[0] == 's')
                this.Opacity = .5;

            if (poptypearr[1] == 't')
            {
                // Set the form click-through
                UInt32 initialStyle = GetWindowLong(this.Handle, -20);
                SetWindowLong(this.Handle, -20, initialStyle | 0x80000 | 0x20);
            }
            else
                if (poptypearr[1] == 'c')
            {
                this.Click += clickable;
                axWindowsMediaPlayer1.ClickEvent += AxWindowsMediaPlayer1_ClickEvent; ;
            }

            if ((poptypearr[2] == 'm') && (poptypearr[3] != 'n'))
            {
                System.Windows.Forms.Timer tmr2 = new System.Windows.Forms.Timer();
                tmr2.Interval = (int)TimeSpan.FromSeconds(2).TotalMilliseconds;
                tmr2.Tick += popup_tick2;
                tmr2.Start();
            }

            if (Utils.IsWebPage(url))
            {
                System.Diagnostics.Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
                this.Close();
            }
            else
            {
                run_url = url;
            }
            tmr = new System.Windows.Forms.Timer();
            tmr.Tick += popup_tick;
            Random rand = new Random();
            if (ConfigurationManager.AppSettings["PopSet"] != null)
            {
                string lengthtime = ConfigurationManager.AppSettings["PopSet"].ToString();
                if ("Long" == lengthtime)
                {
                    int mins = rand.Next(11);
                    tmr.Interval = (int)TimeSpan.FromMinutes(mins).TotalMilliseconds;
                }
                else
                {
                    int mins = rand.Next(55) + 5;
                    tmr.Interval = (int)TimeSpan.FromSeconds(mins).TotalMilliseconds;
                }
            }
        }

        private void AxWindowsMediaPlayer1_ClickEvent(object sender, AxWMPLib._WMPOCXEvents_ClickEvent e)
        {
            this.Close();
        }

        public void add_url(string url)
        {
            urls.Add(url);
        }
        char lastchar = 'n';
        public void clickable(object sender, EventArgs e)
        {
            this.Close();
        }
        public void popup_tick2(object sender, EventArgs e)
        {
            Random random = new Random();
            int screenWidth = Screen.PrimaryScreen.Bounds.Width;
            int screenHeight = Screen.PrimaryScreen.Bounds.Height;
            int randomX = random.Next(0, screenWidth - this.Width);
            int randomY = random.Next(0, screenHeight - this.Height);
            this.Location = new Point(randomX, randomY);
        }
        public void popup_tick(object sender, EventArgs e)
        {

            if (urls.Count > 0)
            {
                run_url = urls[0];
                urls.RemoveAt(0);
                axWindowsMediaPlayer1.Visible = true;
                axWindowsMediaPlayer1.URL = run_url;
                axWindowsMediaPlayer1.Ctlenabled = false;
                axWindowsMediaPlayer1.uiMode = "None";
                axWindowsMediaPlayer1.stretchToFit = true;
                axWindowsMediaPlayer1.settings.autoStart = true;
                axWindowsMediaPlayer1.settings.setMode("loop", true);
            }
            else
            {
                this.Close();
            }
        }

        private void PopUp_Load(object sender, EventArgs e)
        {
            if (poptypearr[3] == 'f')
            {
                this.WindowState = FormWindowState.Maximized;

            }
            else
            {
                Random random = new Random();
                int screenWidth = Screen.PrimaryScreen.Bounds.Width;
                int screenHeight = Screen.PrimaryScreen.Bounds.Height;
                int randomX = random.Next(0, screenWidth - this.Width);
                int randomY = random.Next(0, screenHeight - this.Height);
                this.StartPosition = FormStartPosition.Manual;
                this.Location = new Point(randomX, randomY);
            }

            axWindowsMediaPlayer1.URL = run_url;
            axWindowsMediaPlayer1.Ctlenabled = false;
            axWindowsMediaPlayer1.uiMode = "None";
            axWindowsMediaPlayer1.stretchToFit = true;
            axWindowsMediaPlayer1.settings.autoStart = true;
            axWindowsMediaPlayer1.settings.setMode("loop", true);

            tmr.Start();

        }
    }
}
