using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlApp
{
    public partial class SubLoop : Form
    {
        const int GWL_STYLE = (-20);

        const UInt32 WS_POPUP = 0x80000000;
        const UInt32 WS_CHILD = 0x20000000;
        private System.Windows.Forms.Timer tmr;

        [DllImport("user32.dll", SetLastError = true)]
        static extern UInt32 GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, UInt32 dwNewLong);

        List<string[]> itemstoloop;
        Utils Utils;
        string appDirectory;
        int position;
        public SubLoop()
        {

            position = 0;
            InitializeComponent();
            label1.Text = "";
            itemstoloop = new List<string[]>();
            Utils = new Utils();
            string[] filecontent = new string[1];
            appDirectory = AppDomain.CurrentDomain.BaseDirectory + @"\ConstantSubList.txt";
            if (File.Exists(appDirectory))
            {
                filecontent = File.ReadAllLines(appDirectory);
                foreach (string file in filecontent)
                {
                    try
                    {
                        itemstoloop.Add(Utils.seperate_string(file));
                    }
                    catch { }
                }
            }
            tmr = new System.Windows.Forms.Timer();
            tmr.Tick += loopthrough;
            double time = 0.25;
            tmr.Interval = (int)TimeSpan.FromMinutes(time).TotalMilliseconds;

            // Set the form click-through
            UInt32 initialStyle = GetWindowLong(this.Handle, -20);
            SetWindowLong(this.Handle, -20, initialStyle | 0x80000 | 0x20);

            this.BackColor = Color.Magenta; // Set the background color to an uncommon color
            this.TransparencyKey = Color.Magenta; // Make that color transparent
            tmr.Start();
        }

        public void loopthrough(object sender, EventArgs e)
        {
            int total = itemstoloop.Count;
            if (position >= total)
            {
                position = 0;
            }
            if (itemstoloop.Count > 0)
            {
                if ((axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsStopped)|| (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsUndefined))
                {
                    string[] item = itemstoloop[position];
                    bool mess = true;
                    try
                    {
                        if (item[0] == "m")
                            mess = false;
                        doitem(item[1], mess);
                    }
                    catch { }
                    position++;
                }
            }
        }

        public void additem(string item)
        {

            if (!Utils.IsWebPage(item))
            {
                //download item if needed
                itemstoloop.Add(new string[2] { "t", item });
            }
            else
            {
                //if message add to list
                item = Utils.Get_File(item);
                if (item != "FAILED")
                    itemstoloop.Add(new string[2] { "m", item });
            }

        }
        public void doitem(string item, bool message)
        {
            if (message)
            {
                label1.Visible = true;
                label1.Text = item;
                axWindowsMediaPlayer1.Visible = false;
            }
            else
            {
                label1.Visible = false;
                axWindowsMediaPlayer1.Visible = true;
                axWindowsMediaPlayer1.URL = item;
                axWindowsMediaPlayer1.Ctlenabled = false;
                axWindowsMediaPlayer1.stretchToFit = true;
                axWindowsMediaPlayer1.uiMode = "None";
                axWindowsMediaPlayer1.settings.autoStart = true;
                axWindowsMediaPlayer1.settings.volume = 10;
            }
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            File.Delete(appDirectory);
            using (StreamWriter writer = File.AppendText(appDirectory))
            {
                foreach (string[] items in itemstoloop)
                {
                    writer.WriteLine("[" + items[0] + "],[" + items[1] + "]");
                }
            }
            tmr.Stop();
        }

        private void SubLoop_Load(object sender, EventArgs e)
        {

        }
    }
}
