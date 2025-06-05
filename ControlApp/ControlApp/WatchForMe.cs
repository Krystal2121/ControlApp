using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;
using AxWMPLib;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection.Emit;
using System.Configuration;
using System.Runtime.InteropServices;
using System.Data.SqlTypes;


namespace ControlApp
{
    public partial class WatchForMe : Form
    {
        string senderstr;
        string pub_Url;

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool InternetSetOption(IntPtr hInternet, int dwOption, string lpBuffer, int lpdwBufferLength);

        private const int INTERNET_OPTION_USER_AGENT = 41; // User-Agent option


        private void SetUserAgent(string userAgent)
        {
            // Use InternetSetOption to set the custom User-Agent
            InternetSetOption(IntPtr.Zero, INTERNET_OPTION_USER_AGENT, userAgent, userAgent.Length);
        }
        static bool IsWebPage(string input)
        {
            if (Uri.TryCreate(input, UriKind.Absolute, out Uri uriResult))
            {
                return uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps;
            }
            return false;
        }
        Utils u;
        DirectoryInfo vlcdir;
        public WatchForMe(string Url, string senderid)
        {
            u = new Utils();
            senderstr = senderid;
            SetUserAgent("Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:109.0) Gecko/20100101 Firefox/109.0");
            vlcdir = new DirectoryInfo(@"C:\Program Files\VideoLAN\VLC");
            InitializeComponent();
            textBox1.Focus();
            string filetype = Path.GetExtension(Url);

            if ((filetype == ".mp4") || (filetype == ".webm") || (filetype == ".webp") || (filetype == ".gif"))
            {

                pub_Url = Url;

                axWindowsMediaPlayer1.Ctlenabled = false;
                axWindowsMediaPlayer1.uiMode = "None";
                axWindowsMediaPlayer1.settings.autoStart = true;
                axWindowsMediaPlayer1.settings.setMode("loop", true);
                if (IsWebPage(Url))
                {
                    string getthefile = u.Get_File(Url);
                    if (getthefile != "FAILED")
                    {
                        FileInfo fileInfo = new FileInfo(Url);
                        axWindowsMediaPlayer1.URL = fileInfo.ToString();

                    }
                    else
                        axWindowsMediaPlayer1.URL = Url;
                }
                else
                {
                    FileInfo fileInfo = new FileInfo(Url);
                    axWindowsMediaPlayer1.URL = fileInfo.ToString();
                }
                webView21.Visible = false;

            }
            else if ((filetype == ".png") || (filetype == ".jpg") || (filetype == ".jepg"))
            {
                string file = "";
                webView21.Visible = false;
                file = u.Get_File(Url);
                if (file == "FAILED")
                    this.Close();
                else
                {
                    System.Drawing.Image i = System.Drawing.Image.FromFile(file);
                    Screen my = Screen.AllScreens[0];
                    if (i.Width > my.Bounds.Width)
                        this.Width = my.Bounds.Width;
                    else
                        this.Width = i.Width;

                    if (i.Height > my.Bounds.Height)
                        this.Height = my.Bounds.Height;
                    else
                        this.Height = i.Height;

                    axWindowsMediaPlayer1.Visible = false;
                    PictureBox pictureBox = new PictureBox()
                    {
                        Dock = DockStyle.Fill,
                        Image = i
                    };
                    Controls.Add(pictureBox);
                }
            }
            else
            {
                axWindowsMediaPlayer1.Visible = false;
                if (Url.Substring(0, 4).ToUpper() != "HTTP")
                {
                    Url = "http://" + Url;
                }
                // Convert string to URI
                Uri uri = new Uri(Url);

                webView21.Source = uri;
            }
            this.AutoSize = true;
            timewatched = 0;
            timecensored = 0;
            losefocus = 0;
            WatchTime.Interval = 1000; // Set the timer interval to 1 second
            WatchTime.Tick += new EventHandler(Timer1_Tick);
            WatchTime.Start();
            CensorTime.Interval = 1000;
            CensorTime.Tick += new EventHandler(Censored_Tick);
            isdeactive = false;
            this.Deactivate += new EventHandler(Form1_Deactivate);
            this.Activated += new EventHandler(Form1_Reactivate);
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            textBox1.SendToBack();
        }
        int timewatched;
        int timecensored;
        bool isdeactive;
        int losefocus;

        private void WatchForMe_Load(object sender, EventArgs e)
        {
            censored = false;
        }
        private void Censored_Tick(object sender, EventArgs e)
        {
            timecensored++;
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (!isdeactive)
            {
                textBox1.Focus();
                timewatched++;
                this.Text = "Watch for me Time=" + timewatched.ToString() + ": Censored for=" + timecensored.ToString() + ": Lost focus=" + losefocus.ToString();
            }
        }
        private void Form1_Deactivate(object sender, EventArgs e)
        {
            losefocus++;
            isdeactive = true;
        }
        private void Form1_Reactivate(object sender, EventArgs e)
        {
            if (isdeactive)
            {
                isdeactive = false;
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                string usrnm = ConfigurationManager.AppSettings["UserName"].ToString();
                Utils utils = new Utils();
                utils.sendcmd(senderstr, utils.Ecrypt("M=User : " + usrnm + " Watched for : " + timewatched.ToString() + " seconds. Censored for : " + timecensored.ToString() + ". Form lost focus :" + losefocus.ToString() + " times"), false);

            }
        }
        bool censored;
        public void dokeys()
        { }
        private void WFM_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.C)
            {
                if (censored)
                {
                    this.Opacity = 1;
                    censored = false;
                    CensorTime.Stop();
                }
                else
                {
                    this.Opacity = 0;
                    censored = true;
                    CensorTime.Start();
                }

            }
        }

        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'c')
            {
                if (censored)
                {
                    this.Opacity = 1;
                    censored = false;
                    CensorTime.Stop();
                }
                else
                {
                    this.Opacity = 0;
                    censored = true;
                    CensorTime.Start();
                }

            }
        }

    }
}
