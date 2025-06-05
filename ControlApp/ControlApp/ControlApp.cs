
using Emgu.CV;
using Emgu.CV.Dnn;
using Emgu.CV.Features2D;
using Microsoft.VisualBasic.ApplicationServices;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ControlApp
{
    public partial class ControlApp : Form
    {
        Utils utils;
        bool varified;
        string[] bannedsights;
        public ControlApp()
        {
            InitializeComponent();
            utils = new Utils();
            string blist = ConfigurationManager.AppSettings["BlackList"];
            if (blist != null)
            {
                touser.Items.Clear();
                utils.blist = utils.seperate_string(blist);
            }
            varified = false;
            string ld = ConfigurationManager.AppSettings["LocalDrive"];
            if ((ld == null) || (ld == ""))
            {
                Configuration myconfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                KeyValueConfigurationCollection apps = myconfig.AppSettings.Settings;
                string dl = GetDownloadsPath() + "\\";
                apps.Remove("LocalDrive");
                apps.Add("LocalDrive", dl);
                myconfig.Save(ConfigurationSaveMode.Full);
                ConfigurationManager.RefreshSection(myconfig.AppSettings.SectionInformation.Name);
            }
            else if (ld.Substring(ld.Length - 1, 1) != "\\")
            {
                Configuration myconfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                KeyValueConfigurationCollection apps = myconfig.AppSettings.Settings;
                string dl = ld + "\\";
                apps.Remove("LocalDrive");
                apps.Add("LocalDrive", dl);
                myconfig.Save(ConfigurationSaveMode.Full);
                ConfigurationManager.RefreshSection(myconfig.AppSettings.SectionInformation.Name);
            }
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            bannedsights = new string[] { "booru.allthefallen.moe", "mega.nz", "media.mstdn.jp", "thecontrolapp.co.uk/Pages/ControlPC", "paradroid-gamma.vercel", "imagekit.io/tools/asset-public-link", "paradroid-gamma.web.app" };
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(groupcmbx, "Sends the command to all in this group.");
            toolTip.SetToolTip(touser, "Sends the command to this user.");
            meuser.Text = ConfigurationManager.AppSettings["UserName"].ToString();
            if (ConfigurationManager.AppSettings["DarkMode"] != null)
            {
                string dmode = ConfigurationManager.AppSettings["DarkMode"].ToString();
                if (Convert.ToBoolean(dmode))
                {
                    this.BackColor = Color.DarkGray;
                    this.ForeColor = Color.White;
                }
            }
        }

        private void configToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigSettingsForm form = new ConfigSettingsForm();
            form.ShowDialog();
            meuser.Text = ConfigurationManager.AppSettings["UserName"].ToString();
        }

        BackgroundWorker bg;
        string[] currentsender;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (ConfigurationManager.AppSettings["RunAll"] == "True")
            {
                currentsender = utils.GetLatestItem();
            }
            chk_next();
        }
        private void button11_Click(object sender, EventArgs e)
        {
            string[] strings = MainTxt.Text.Split("|||");
            string ret = "";
            foreach (string s in strings)
            {
                ret += utils.Decrypt(s) + "|||";
            }
            MainTxt.Text = ret;
        }
        private static Guid FolderDownloads = new Guid("374DE290-123F-4565-9164-39C4925E467B");
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        private static extern int SHGetKnownFolderPath(ref Guid id, int flags, IntPtr token, out IntPtr path);

        public static string GetDownloadsPath()
        {
            if (Environment.OSVersion.Version.Major < 6) throw new NotSupportedException();

            IntPtr pathPtr = IntPtr.Zero;

            try
            {
                SHGetKnownFolderPath(ref FolderDownloads, 0, IntPtr.Zero, out pathPtr);
                return Marshal.PtrToStringUni(pathPtr);
            }
            finally
            {
                Marshal.FreeCoTaskMem(pathPtr);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                if (ConfigurationManager.AppSettings["DarkMode"] != null)
                {
                    string dmode = ConfigurationManager.AppSettings["DarkMode"].ToString();
                    if (Convert.ToBoolean(dmode))
                    {
                        this.BackColor = Color.Black;
                        this.ForeColor = Color.White;
                        foreach (Control control in this.Controls)
                        {
                            if (control is Panel)
                            {
                                control.BackColor = Color.Black;
                                control.ForeColor = Color.White;
                            }
                            if (control is Button)
                            {
                                control.BackColor = Color.DarkGray;
                                control.ForeColor = Color.White;
                            }

                        }
                        foreach (TabPage tabPage in tabControl1.TabPages)
                        {
                            tabPage.BackColor = Color.Black;
                            tabPage.ForeColor = Color.White;
                            foreach (Control control2 in tabPage.Controls)
                            {
                                if (control2 is Panel)
                                {
                                    control2.BackColor = Color.Black;
                                    control2.ForeColor = Color.White;
                                }
                                if (control2 is Button)
                                {
                                    control2.BackColor = Color.DarkGray;
                                    control2.ForeColor = Color.White;
                                }
                            }
                        }
                    }
                }
                string userlist = ConfigurationManager.AppSettings["CommonUsers"];
                if (userlist != null)
                {
                    touser.Items.Clear();
                    string[] users = utils.seperate_string(userlist);
                    foreach (string user in users)
                    {
                        touser.Items.Add(user);
                    }
                }
                using (CustomMessage cm = new CustomMessage("Initialising, please wait", "", 0, false))
                {
                    cm.Show();
                    chk_next();
                }
                int delay;
                if (ConfigurationManager.AppSettings["Delay"] == null)
                { delay = 60; }
                else { delay = Convert.ToInt32(ConfigurationManager.AppSettings["Delay"]); }
                //Task.Run(async () => await chk_next_async());
                timer1.Enabled = true;
                timer1.Interval = delay * 1000;
            }
            catch
            { }
            //load dom message log file
        }

        public void chk_next()
        {
            Cursor.Show();
            Cursor cursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            try
            {


                //string geturl = "https://www.thecontrolapp.co.uk/GetCount.aspx?UserNm=" + user + "&Pwd=" + utils.Ecrypt(pwd) + "&vrs=" + vrs;
                string user = ConfigurationManager.AppSettings["UserName"].ToString();
                string pwd = ConfigurationManager.AppSettings["Password"].ToString();
                string vrs = "012";
                string result = utils.getcmd(user, pwd, vrs, "Outstanding");
                string[] ret = utils.seperate_string(result);

                Nocomds.Text = ret[0];
                whonxtlbl.Text = ret[1];
                if (ret[2] == "0")
                    varified = false;
                else varified = true;
                ScoreTxt.Text = ret[3];

                if (Nocomds.Text != "0")
                {
                    try
                    {
                        if (ConfigurationManager.AppSettings["OutstandRemind"] == null || !Convert.ToBoolean(ConfigurationManager.AppSettings["OutstandRemind"].ToString()))
                        {
                            if (Nocomds.Text == "1")
                            {
                                CustomMessage cm = new CustomMessage("You have " + Nocomds.Text + " outstanding command.", "", 0, true);
                                cm.Show();
                            }
                            else if (Convert.ToInt16(Nocomds.Text) > 1)
                            {
                                CustomMessage cm = new CustomMessage("You have " + Nocomds.Text + " outstanding commands.", "", 0, true);
                                cm.Show();
                            }

                        }
                    }
                    catch { }
                }
            }
            catch (Exception ex)
            {
                utils.writetolog("Error checking for count", "Error checking for count :" + ex.Message);
            }
            Cursor.Current = cursor;
        }
        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Options options = new Options();
            options.ShowDialog();
        }
        public void createmessageline(string code)
        {
            string line = "";
            if (webtxt.Text == "")
            {
                if (filetxt.Text == "")
                    MessageBox.Show("Please enter a file name.");
                else
                {
                    line = code + "=https://www.thecontrolapp.co.uk/storage/" + filetxt.Text;
                }
            }
            else
            {
                foreach (string banned in bannedsights)
                {
                    if (webtxt.Text.Contains(banned))
                    {
                        MessageBox.Show("Website banned");
                        break;
                    }
                    else
                    {
                        line = code + "=" + webtxt.Text;
                    }
                }
            }
            line = utils.Ecrypt(line);
            MainTxt.Text += line + "|||";
            webtxt.Text = "";
            filetxt.Text = "";
        }
        private void DownloadFile_Btn_Click(object sender, EventArgs e)
        {
            createmessageline("D");
        }
        private void ChangeWall_Btn_Click(object sender, EventArgs e)
        {
            string file = "";
            if (webtxt.Text != "")
                file = webtxt.Text;
            if (filetxt.Text != "")
                file = filetxt.Text;
            try
            {
                string filetype = Path.GetExtension(file);
                if ((filetype == ".jpg") || (filetype == ".jpeg") || (filetype == ".png"))
                    createmessageline("P");
                else
                    MessageBox.Show("Incorrect file type. Please use only image files.");
            }
            catch
            {
                MessageBox.Show("Incorrect file type. Please use only image files.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string file = "";
            if (webtxt.Text != "")
                file = webtxt.Text;
            if (filetxt.Text != "")
                file = filetxt.Text;
            try
            {
                string filetype = Path.GetExtension(file);
                if ((filetype == ".exe") || (filetype == ".bat"))
                    createmessageline("R");
                else
                    MessageBox.Show("Incorrect file type. Please use only exe or Bat");
            }
            catch
            {
                MessageBox.Show("Incorrect file type. Please use only exe or Bat");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            createmessageline("W");
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            string file = "";
            if (webtxt.Text != "")
                file = webtxt.Text;
            if (filetxt.Text != "")
                file = filetxt.Text;
            try
            {
                string filetype = Path.GetExtension(file);
                if ((filetype == ".jpg") || (filetype == ".jpeg") || (filetype == ".webm") || (filetype == ".webp") || (filetype == ".png") || (filetype == ".mp4") || (filetype == ".gif") || (filetype == ".avi"))
                    createmessageline("U");
                else
                    MessageBox.Show("Incorrect file type. Please use only image or movie files");

            }
            catch
            {
                MessageBox.Show("Incorrect file type. Please use only image or movie files");
            }
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            createmessageline("O");
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (msgbx.Text.Length < 2000)
            {
                string line = "M=" + msgbx.Text + "&&&" + btnbx.Text;
                line = utils.Ecrypt(line);
                MainTxt.Text += line + "|||";
            }
            else
                MessageBox.Show("Message is too long");
        }

        private void button13_Click_1(object sender, EventArgs e)
        {
            string line = "V=" + msgbx.Text;
            line = utils.Ecrypt(line);
            MainTxt.Text += line + "|||";
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            string[] lines;
            if (MainTxt.Text.IndexOf("|||") > 0)
            {
                lines = MainTxt.Text.Split("|||");
            }
            else
            {
                lines = new string[] { MainTxt.Text };
            }
            string me = ConfigurationManager.AppSettings["UserName"].ToString();
            utils.run_process(lines, me);
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            Clipboard.SetText(MainTxt.Text);
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            MainTxt.Text = "";
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            try
            {
                string Meuser = ConfigurationManager.AppSettings["UserName"].ToString();
                if (Meuser != "")
                {
                    string usernm = "";
                    string comm = MainTxt.Text;
                    if ((groupcmbx.SelectedIndex == 0) || (groupcmbx.SelectedIndex == -1))
                    {
                        usernm = touser.Text;
                        utils.sendcmd(usernm, comm, true);
                    }
                    else
                    {
                        touser.Text = "";
                        usernm = getgroup().ToString();
                        utils.sendcmd(usernm, comm, false);
                    }
                }
                else
                { MessageBox.Show("Anonymous commands no longer permitted."); }
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                utils.writetolog(msg, msg);
            }
            groupcmbx.SelectedIndex = 0;
            //touser.Text = "";
            MainTxt.Text = "";
            senddelbtn.Enabled = true;
            tkscreen.Enabled = true;
            groupcmbx.Enabled = true;
        }

        private void RunNxt_Click(object sender, EventArgs e)
        {
            currentsender = utils.GetLatestItem();
            chk_next();
            Thumbsupbtn.Enabled = true;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            //Block sender
            if (currentsender[0] != "-1")
            {
                var result = MessageBox.Show("Are you sure", "Sure?", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    utils.SendBlockReport(currentsender[0], "", "0");
                }
            }
            else
            {
                MessageBox.Show("Last message was from an anonymous sender");
            }
        }

        private void RptSend_Click(object sender, EventArgs e)
        {
            //report sender
            var result = MessageBox.Show("Are you sure", "Sure?", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                utils.SendBlockReport(currentsender[0], currentsender[1], "1");
            }
        }

        private void button11_Click_1(object sender, EventArgs e)
        {
            chk_next();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            utils.sendftpfile(filetxt.Text);
        }
        private void button14_Click(object sender, EventArgs e)
        {
            string[] strings = MainTxt.Text.Split("|||");
            MainTxt.Text = "";
            foreach (string s in strings)
            {
                MainTxt.Text += utils.Decrypt(s);
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (varified)
            {
                DialogResult dr = openFileDialog1.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    string fulladdress = openFileDialog1.FileName;
                    filetxt.Text = Path.GetFileName(fulladdress);

                    Webform wf = new Webform("https://www.thecontrolapp.co.uk/upload.aspx?file=" + fulladdress);
                    wf.Show();
                }
                else
                    filetxt.Text = "";
            }
            else
            {
                CustomMessage cm = new CustomMessage("Only allowed for varified users", "", 0, false);
                cm.ShowDialog();
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        public int getgroup()
        {
            int sel = groupcmbx.SelectedIndex;
            return (sel + 1) * -1;
        }
        private void button17_Click(object sender, EventArgs e)
        {
            string file = "";
            if (webtxt.Text != "")
                file = webtxt.Text;
            if (filetxt.Text != "")
                file = filetxt.Text;
            try
            {
                string filetype = Path.GetExtension(file);
                if ((filetype == ".jpg") || (filetype == ".jpeg") || (filetype == ".webp") || (filetype == ".webm") || (filetype == ".png") || (filetype == ".mp4") || (filetype == ".gif") || (filetype == ".avi"))
                    createmessageline("S");
                else
                    MessageBox.Show("Incorrect file type. Please use only image or movie files");
            }
            catch
            {
                MessageBox.Show("Incorrect file type. Please use only image or movie files");
            }
        }

        private void button12_Click_2(object sender, EventArgs e)
        {
            string usernm;
            bool all = false;
            string comm = MainTxt.Text;
            if (groupcmbx.SelectedIndex == 0)
            {
                if (currentsender[0] != "-1")
                    utils.sendcmd(currentsender[0], comm, all);
            }
            else
            {
                if ((currentsender[0] != null) && (Convert.ToInt16(currentsender[0]) >= -1))
                {
                    usernm = getgroup().ToString();
                    all = true;
                    utils.sendcmd(usernm, comm, all);
                }
            }
            senddelbtn.Enabled = true;
        }

        private void wfmbtn_Click(object sender, EventArgs e)
        {
            if (int.TryParse(wfmnotxt.Text, out int x))
            {
                string line = "F=" + wfmtxt.Text + "&&&" + wfmnotxt.Text;
                line = utils.Ecrypt(line);
                MainTxt.Text += line + "|||";
            }
            else
                MessageBox.Show("How many must be a number.");
        }

        private void plaudbtn_Click(object sender, EventArgs e)
        {
            string file = "";
            if (webtxt.Text != "")
                file = webtxt.Text;
            if (filetxt.Text != "")
                file = filetxt.Text;
            try
            {
                string filetype = Path.GetExtension(file);
                if ((filetype == ".mp3") || (filetype == ".wav") || (filetype == ".m4a"))
                    createmessageline("A");
                else
                    MessageBox.Show("Incorrect file type. Please use only sound files");
            }
            catch
            {
                MessageBox.Show("Incorrect file type. Please use only sound files");
            }

        }

        private void button16_Click_1(object sender, EventArgs e)
        {
            MainTxt.Text = utils.Ecrypt(MainTxt.Text);
        }

        private void tkscreen_Click(object sender, EventArgs e)
        {
            if (varified)
            {
                string line = "1=Yes";
                line = utils.Ecrypt(line);
                MainTxt.Text += line + "|||";
                tkscreen.Enabled = false;
            }
            else
            {
                CustomMessage cm = new CustomMessage("Only allowed for varified users", "", 0, false);
                cm.ShowDialog();
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {

        }

        private void ClearOut_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure?", "Sure?", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                string user = ConfigurationManager.AppSettings["UserName"].ToString();
                string pwd = ConfigurationManager.AppSettings["Password"].ToString();
                string vrs = "012";
                utils.deleteoutstanding(user, pwd, vrs);
                chk_next();
            }
        }

        private void wtchfrmebtn_Click(object sender, EventArgs e)
        {
            createmessageline("2");
        }

        private void twitter_Click(object sender, EventArgs e)
        {
            if (msgbx.Text.Length < 2000)
            {
                string line = "3=" + msgbx.Text;
                line = utils.Ecrypt(line);
                MainTxt.Text += line + "|||";
            }
            else
                MessageBox.Show("Message is too long");
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (varified)
            {
                string line = "4=Yes";
                line = utils.Ecrypt(line);
                MainTxt.Text += line + "|||";
            }
            else
            {
                CustomMessage cm = new CustomMessage("Only allowed for varified users", "", 0, false);
                cm.ShowDialog();
            }
            senddelbtn.Enabled = false;
        }

        private void TTSBtn_Click(object sender, EventArgs e)
        {
            if (msgbx.Text.Length < 2000)
            {
                string line = "5=" + msgbx.Text;
                line = utils.Ecrypt(line);
                MainTxt.Text += line + "|||";
            }
            else
                MessageBox.Show("Message is too long");
        }

        private void button14_Click_1(object sender, EventArgs e)
        {
            MainTxt.Text = utils.Ecrypt(MainTxt.Text);
        }

        private void RunLastBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string[] runcode = currentsender[1].Split("|||");
                utils.run_process(runcode, currentsender[0]);
            }
            catch (Exception ex)
            {
                utils.writetolog("Error re-running process \n\r", ex.Message);
            }
        }

        private void webcambtn_Click(object sender, EventArgs e)
        {
            if (varified)
            {
                string line = "6=Yes";
                line = utils.Ecrypt(line);
                MainTxt.Text += line + "|||";
            }
            else
            {
                CustomMessage cm = new CustomMessage("Only allowed for varified users", "", 0, false);
                cm.ShowDialog();
            }
        }

        private void button14_Click_2(object sender, EventArgs e)
        {
            CustomMessage cm = new CustomMessage("", "", 10, false);
            cm.ShowDialog();
        }

        private void disablemsbtn_Click(object sender, EventArgs e)
        {
            if (varified)
            {
                string line = "7=Yes";
                line = utils.Ecrypt(line);
                MainTxt.Text += line + "|||";
            }
            else
            {
                CustomMessage cm = new CustomMessage("Only allowed for varified users", "", 0, false);
                cm.ShowDialog();
            }
        }

        private void groupcmbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            touser.Text = "";
        }

        private async void WaitFiveSeconds()
        {
            await Task.Delay(5000); // Wait for 5 seconds
            MessageBox.Show("5 seconds have passed!");
        }

        private void button14_Click_3(object sender, EventArgs e)
        {
            if (varified)
            {
                string line = "8=Yes";
                line = utils.Ecrypt(line);
                MainTxt.Text += line + "|||";
                groupcmbx.SelectedIndex = 8;
                groupcmbx.Enabled = false;
            }
            else
            {
                CustomMessage cm = new CustomMessage("Only allowed for varified users", "", 0, false);
                cm.ShowDialog();
            }
        }



        private void otherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (Other ot = new Other())
            {
                ot.ShowDialog();
            }
            string userlist = ConfigurationManager.AppSettings["CommonUsers"];
            if (userlist != null)
            {
                touser.Items.Clear();
                string[] users = utils.seperate_string(userlist);
                foreach (string user in users)
                {
                    touser.Items.Add(user);
                }
            }
        }

        private void Thumbsupbtn_Click(object sender, EventArgs e)
        {
            if (currentsender[0] != "-1")
            {
                string user = ConfigurationManager.AppSettings["UserName"].ToString();
                string pwd = ConfigurationManager.AppSettings["Password"].ToString();
                string vrs = "012";
                utils.getcmd(user, pwd, vrs, "Thumbs" + currentsender[0]);
                Thumbsupbtn.Enabled = false;
            }
        }

        private void touser_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupcmbx.Text = "";
            groupcmbx.SelectedIndex = 0;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (msgbx.Text.Length > 0)
            {
                string line = "L=" + msgbx.Text;
                line = utils.Ecrypt(line);
                MainTxt.Text += line + "|||";
                msgbx.Text = "";
            }
            if (webtxt.Text.Length > 0)
            {
                string line = "L=" + webtxt.Text;
                line = utils.Ecrypt(line);
                MainTxt.Text += line + "|||";
                webtxt.Text = "";
            }
            if (filetxt.Text.Length > 0)
            {
                string line = @"L=https://www.thecontrolapp.co.uk/storage/" + filetxt.Text;
                line = utils.Ecrypt(line);
                MainTxt.Text += line + "|||";
                filetxt.Text = "";
            }
        }

        private void button15_Click_1(object sender, EventArgs e)
        {
            Spinner spinner = new Spinner("[test1],[test2],[test3],[test4]");
            spinner.Show();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            string spinline = "";
            for (int i = 0; i < 10 && i < spin_txt.Lines.Length; i++)
            {
                if ((spin_txt.Lines[i] != null) && (spin_txt.Lines[i] != ""))
                {
                    spinline += "[" + spin_txt.Lines[i] + "],";
                }
            }
            if (spinline.Length > 0)
            {
                spinline = "9=" + spinline.Substring(0, spinline.Length - 1);
                spinline = utils.Ecrypt(spinline);
                MainTxt.Text += spinline + "|||";
            }

        }
    }

}
