using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlApp
{
    public partial class SendOrDelete : Form
    {
        string thefile;
        string senderstr;
        public SendOrDelete(string senderid)
        {
            InitializeComponent();
            string location = ConfigurationManager.AppSettings["LocalDrive"];
            string[] filetypes = { ".jpg", ".jpeg", ".gif", ".mov", ".mpg", ".mpeg", ".avi", ".png", ".mp4" };
            List<string> acceptablefiles= new List<string>();
            if (location != null)
            {
                string[] allfiles = Directory.GetFiles(location);
                foreach (var file in allfiles)
                {
                    FileInfo info = new FileInfo(file);
                    if (filetypes.Contains(info.Extension) && info.Length < 1000000)
                    {
                        acceptablefiles.Add(file);
                    }
                }
            }
            int max = acceptablefiles.Count();
            if (max > 0)
            {
                Random rnd = new Random();
                thefile = acceptablefiles[rnd.Next(max)];
                senderstr = senderid;
            }
        }

        private void Deletebtn_Click(object sender, EventArgs e)
        {
            if (thefile != null)
            {
                File.Delete(thefile);
            }
            Utils utils = new Utils();

            string usrnm = ConfigurationManager.AppSettings["UserName"].ToString();
            string line1 = "M=" + usrnm + " chose to delete.";
            utils.sendcmd(senderstr, utils.Ecrypt(line1) , false);
            this.Close();
        }

        private void SendOrDelete_Load(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.URL = thefile;
            axWindowsMediaPlayer1.Ctlenabled = false;
            axWindowsMediaPlayer1.uiMode = "None";
            axWindowsMediaPlayer1.settings.autoStart = true;
            axWindowsMediaPlayer1.settings.setMode("loop", true);
        }

        private void Sendbtn_Click(object sender, EventArgs e)
        {
            Utils utils= new Utils();
            bool wrked = utils.sendftpfile(thefile);
            if (wrked)
            {
                string usrnm = ConfigurationManager.AppSettings["UserName"].ToString();
                string line1 = "M=" + usrnm + " chose to send.";
                string line2 = "U=FTP" + Path.GetFileName(thefile);
                utils.sendcmd(senderstr, utils.Ecrypt(line1) + "|||" + utils.Ecrypt(line2), false);
            }
            this.Close();
        }
    }
}
