using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ControlApp
{
    public partial class Options : Form
    {
        public Options()
        {
            InitializeComponent();
        }

        private void SvExit_Click(object sender, EventArgs e)
        {
            Configuration myconfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            KeyValueConfigurationCollection apps = myconfig.AppSettings.Settings;

            //sub options
            apps.Remove("Downloads");
            apps.Remove("Wallpapers");
            apps.Remove("Runables");
            apps.Remove("OpenWeb");
            apps.Remove("PopUps");
            apps.Remove("Messages");
            apps.Remove("Subliminals");
            apps.Remove("PopSet");
            apps.Remove("Audios");
            apps.Remove("Writeformes");
            apps.Remove("Screenshots");
            apps.Remove("Watch4me");
            apps.Remove("twitter");
            apps.Remove("PaperStyle");
            apps.Remove("Popstyle");
            apps.Remove("SendDelete");
            apps.Remove("OutstandRemind");
            apps.Remove("PopType");
            apps.Remove("Showblocked");
            apps.Remove("Webcam");
            apps.Remove("TTS");
            apps.Remove("DisM");
            apps.Remove("WebCnt");
            apps.Remove("DarkMode");
            if (dmode_chk.Checked == true)
                apps.Add("DarkMode", "True");
            else
                apps.Add("DarkMode", "False");

            if (showblocked.Checked == true)
                apps.Add("Showblocked", "True");
            else
                apps.Add("Showblocked", "False");

            string poptypestr = "";

            if (nseethr.Checked == true) poptypestr = "n";
            else if (seethr.Checked == true) poptypestr = "s";

            if (clickthr.Checked == true)
                poptypestr += "t";
            else if (clkbl.Checked)
                poptypestr += "c";
            else if (normlbtn.Checked)
                poptypestr += "n";

            if (nmove.Checked)
                poptypestr += "n";
            else if (ymove.Checked)
                poptypestr += "m";

            if (fullscchk.Checked)
                poptypestr += "f";
            else
                poptypestr += "n";

            apps.Add("PopType", poptypestr);
            string OutstandRemind = outstandch.Checked.ToString();
            apps.Add("OutstandRemind", OutstandRemind);
            if (serialrd.Checked)
            {
                apps.Add("Popstyle", "Serial");
            }
            else
                apps.Add("Popstyle", "Parallel");
            if (stret.Checked)
            {
                apps.Add("PaperStyle", "Stretch");
            }
            else
                apps.Add("PaperStyle", "Fit");
            string senddel = senddelch.Checked.ToString();
            apps.Add("SendDelete", senddel);
            string twitter = twitch.Checked.ToString();
            apps.Add("twitter", twitter);
            string Watch4me = watch4mech.Checked.ToString();
            apps.Add("Watch4me", Watch4me);
            string Screenshots = screenshch.Checked.ToString();
            apps.Add("Screenshots", Screenshots);
            string Audios = audioch.Checked.ToString();
            apps.Add("Audios", Audios);
            string Writeformes = wrich.Checked.ToString();
            apps.Add("Writeformes", Writeformes);
            string downloads = dlch.Checked.ToString();
            apps.Add("Downloads", downloads);
            string wallp = wallpch.Checked.ToString();
            apps.Add("Wallpapers", wallp);
            string runn = runch.Checked.ToString();
            apps.Add("Runables", runn);
            string webb = opwebch.Checked.ToString();
            apps.Add("OpenWeb", webb);
            string pops = popch.Checked.ToString();
            apps.Add("PopUps", pops);
            string messes = messch.Checked.ToString();
            apps.Add("Messages", messes);
            string sublims = sublimch.Checked.ToString();
            apps.Add("Subliminals", sublims);
            string webc = webcamch.Checked.ToString();
            apps.Add("Webcam", webc);
            string tts = ttsch.Checked.ToString();
            apps.Add("TTS", tts);
            string dism = dismch.Checked.ToString();
            apps.Add("DisM", dism);
            if (longpop.Checked)
            {
                apps.Add("PopSet", "Long");
            }
            else
                apps.Add("PopSet", "Short");
            string webcntch = webcnt.Checked.ToString();
            apps.Add("WebCnt", dism);

            myconfig.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection(myconfig.AppSettings.SectionInformation.Name);
            this.Close();
        }

        private void Options_Load(object sender, EventArgs e)
        {
            //sub
            string style = "Fit";
            string popstyle = "Serial";

            if (ConfigurationManager.AppSettings["DarkMode"] != null)
            {
                string dmode = ConfigurationManager.AppSettings["DarkMode"].ToString();
                
                if (Convert.ToBoolean(dmode))
                {
                    dmode_chk.Checked = true;
                    this.BackColor = Color.Black;
                    this.ForeColor = Color.White;
                    foreach (Control control in this.Controls)
                    {
                        if (control is Panel)
                        {
                            control.BackColor = Color.Black;
                            control.ForeColor = Color.White;
                        }
                        if (control is System.Windows.Forms.Button)
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
                            if (control2 is System.Windows.Forms.Button)
                            {
                                control2.BackColor = Color.DarkGray;
                                control2.ForeColor = Color.White;
                            }
                        }
                    }
                }
            }


            if (ConfigurationManager.AppSettings["PopStyle"] != null)
                popstyle = ConfigurationManager.AppSettings["PopStyle"].ToString();
            if (popstyle == "Serial")
            {
                serialrd.Checked = true;
                parallelrd.Checked = false;
            }
            else
            {
                serialrd.Checked = false;
                parallelrd.Checked = true;
            }

            if (ConfigurationManager.AppSettings["PaperStyle"] != null)
                style = ConfigurationManager.AppSettings["PaperStyle"].ToString();
            if (style == "Fit")
            {
                fitsc.Checked = true;
                stret.Checked = false;
            }
            else
            {
                fitsc.Checked = false;
                stret.Checked = true;
            }
            string poptypestr = "nnnn";
            if (ConfigurationManager.AppSettings["PopType"] != null)
            {
                poptypestr = ConfigurationManager.AppSettings["PopType"];
            }
            char[] poptypearr = poptypestr.ToCharArray();

            if (poptypearr[0] == 'n')
                nseethr.Checked = true;
            else if (poptypearr[0] == 's')
                seethr.Checked = true;

            if (poptypearr[1] == 't')
                clickthr.Checked = true;
            else
                if (poptypearr[1] == 'c')
                clkbl.Checked = true;
            else
                if (poptypearr[1] == 'n')
                normlbtn.Checked = true;


            if (poptypearr[2] == 'n')
                nmove.Checked = true;
            else if (poptypearr[2] == 'm')
                ymove.Checked = true;


            if (poptypearr[3] == 'f')
                fullscchk.Checked = true;
            else
                fullscchk.Checked = false;

            if (ConfigurationManager.AppSettings["Showblocked"] == "True")
                showblocked.Checked = true;
            else
                showblocked.Checked = false;
            string pop = "Short";
            if (ConfigurationManager.AppSettings["PopSet"] != null)
                pop = ConfigurationManager.AppSettings["PopSet"].ToString();
            if (pop == "Long")
            {
                longpop.Checked = true;
                shortpop.Checked = false;
            }
            else
            {
                longpop.Checked = false;
                shortpop.Checked = true;
            }
            if (ConfigurationManager.AppSettings["OutstandRemind"] != null)
            {
                string OutstandRemind = ConfigurationManager.AppSettings["OutstandRemind"].ToString();
                outstandch.Checked = Convert.ToBoolean(OutstandRemind);
            }
            if (ConfigurationManager.AppSettings["SendDelete"] != null)
            {
                string SendDelete = ConfigurationManager.AppSettings["SendDelete"].ToString();
                senddelch.Checked = Convert.ToBoolean(SendDelete);
            }
            if (ConfigurationManager.AppSettings["WebCnt"] != null)
            {
                string WebCnt = ConfigurationManager.AppSettings["WebCnt"].ToString();
                webcnt.Checked = Convert.ToBoolean(WebCnt);
            }
            if (ConfigurationManager.AppSettings["twitter"] != null)
            {
                string twitter = ConfigurationManager.AppSettings["twitter"].ToString();
                twitch.Checked = Convert.ToBoolean(twitter);
            }
            if (ConfigurationManager.AppSettings["Watch4me"] != null)
            {
                string Watch4me = ConfigurationManager.AppSettings["Watch4me"].ToString();
                watch4mech.Checked = Convert.ToBoolean(Watch4me);
            }
            if (ConfigurationManager.AppSettings["Audios"] != null)
            {
                string Audios = ConfigurationManager.AppSettings["Audios"].ToString();
                audioch.Checked = Convert.ToBoolean(Audios);
            }
            if (ConfigurationManager.AppSettings["Writeformes"] != null)
            {
                string Writeformes = ConfigurationManager.AppSettings["Writeformes"].ToString();
                wrich.Checked = Convert.ToBoolean(Writeformes);
            }
            if (ConfigurationManager.AppSettings["Downloads"] != null)
            {
                string downloads = ConfigurationManager.AppSettings["Downloads"].ToString();
                dlch.Checked = Convert.ToBoolean(downloads);
            }
            if (ConfigurationManager.AppSettings["Wallpapers"] != null)
            {
                string wallp = ConfigurationManager.AppSettings["Wallpapers"].ToString();
                wallpch.Checked = Convert.ToBoolean(wallp);
            }
            if (ConfigurationManager.AppSettings["Runables"] != null)
            {
                string runn = ConfigurationManager.AppSettings["Runables"].ToString();
                runch.Checked = Convert.ToBoolean(runn);
            }
            if (ConfigurationManager.AppSettings["ScreenShots"] != null)
            {
                string scre = ConfigurationManager.AppSettings["ScreenShots"].ToString();
                screenshch.Checked = Convert.ToBoolean(scre);
            }
            if (ConfigurationManager.AppSettings["OpenWeb"] != null)
            {
                string webb = ConfigurationManager.AppSettings["OpenWeb"].ToString();
                opwebch.Checked = Convert.ToBoolean(webb);
            }
            if (ConfigurationManager.AppSettings["PopUps"] != null)
            {
                string pops = ConfigurationManager.AppSettings["PopUps"].ToString();
                popch.Checked = Convert.ToBoolean(pops);
            }
            if (ConfigurationManager.AppSettings["Messages"] != null)
            {
                string messes = ConfigurationManager.AppSettings["Messages"].ToString();
                messch.Checked = Convert.ToBoolean(messes);
            }
            if (ConfigurationManager.AppSettings["Subliminals"] != null)
            {
                string sublims = ConfigurationManager.AppSettings["Subliminals"].ToString();
                sublimch.Checked = Convert.ToBoolean(sublims);
            }
            if (ConfigurationManager.AppSettings["Webcam"] != null)
            {
                string Webcam = ConfigurationManager.AppSettings["Webcam"].ToString();
                webcamch.Checked = Convert.ToBoolean(Webcam);
            }
            if (ConfigurationManager.AppSettings["TTS"] != null)
            {
                string TTS = ConfigurationManager.AppSettings["TTS"].ToString();
                ttsch.Checked = Convert.ToBoolean(TTS);
            }
            if (ConfigurationManager.AppSettings["DisM"] != null)
            {
                string dism = ConfigurationManager.AppSettings["DisM"].ToString();
                dismch.Checked = Convert.ToBoolean(dism);
            }

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void webcamch_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void fullscchk_CheckedChanged(object sender, EventArgs e)
        {
            if (fullscchk.Checked)
            {
                serialrd.Checked = true;
            }
        }

        private void parallelrd_CheckedChanged(object sender, EventArgs e)
        {
            if (parallelrd.Checked)
                fullscchk.Checked = false;
        }
    }
}
