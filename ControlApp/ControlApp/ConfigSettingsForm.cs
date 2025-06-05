using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ControlApp
{
    public partial class ConfigSettingsForm : Form
    {
        public ConfigSettingsForm()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Configuration myconfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            KeyValueConfigurationCollection apps = myconfig.AppSettings.Settings;
            apps.Remove("LocalDrive");
            apps.Remove("AutoRun");
            apps.Remove("UserName");
            apps.Remove("Password");
            apps.Remove("Delay");
            apps.Remove("RunAll");
            apps.Add("LocalDrive", textBox1.Text);
            if (checkBox1.Checked == true)
                apps.Add("AutoRun", "True");
            else
                apps.Add("AutoRun", "False");
            apps.Add("UserName", textBox2.Text);
            apps.Add("Password", textBox3.Text);
            if (checkBox2.Checked == true)
                apps.Add("RunAll", "True");
            else
                apps.Add("RunAll", "False");
            if (delaycmb.SelectedIndex == 0)
            {
                apps.Add("Delay", "30");
            }
            else if (delaycmb.SelectedIndex == 1)
            {
                apps.Add("Delay", "60");
            }
            else if (delaycmb.SelectedIndex == 2)
            {
                apps.Add("Delay", "120");
            }
            myconfig.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection(myconfig.AppSettings.SectionInformation.Name);
            this.Close();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            textBox1.Text = ConfigurationManager.AppSettings["LocalDrive"];
            textBox2.Text = ConfigurationManager.AppSettings["UserName"];
            textBox3.Text = ConfigurationManager.AppSettings["Password"];
            if (ConfigurationManager.AppSettings["AutoRun"] == "True")
                checkBox1.Checked = true;
            else
                checkBox1.Checked = false;
            if (ConfigurationManager.AppSettings["RunAll"] == "True")
                checkBox2.Checked = true;
            else
                checkBox2.Checked = false;
            string delstr = ConfigurationManager.AppSettings["Delay"];
            if (delstr=="30")
            {
                delaycmb.SelectedIndex = 0;
            }
            else if (delstr == "60")
            {
                delaycmb.SelectedIndex = 1;
            }
            else if (delstr == "120")
            {
                delaycmb.SelectedIndex = 2;
            }
        }

        private void ConfigSettingsForm_Load(object sender, EventArgs e)
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
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string Password = ConfigurationManager.AppSettings["Password"];
            if ((Password != null) || (Password == ""))
            {
                Password = textBox3.Text;
            }
            string user = ConfigurationManager.AppSettings["UserName"];
            if ((user != null) || (user == ""))
            {
                user = textBox2.Text;
            }
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = "https://www.thecontrolapp.co.uk/Pages/Sub/SubSettings.aspx?user=" + user + "&password=" + Password,
                UseShellExecute = true
            });
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.SetValue("ControlApp", "\"" + Application.ExecutablePath + "\"");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.DeleteValue("My Program", false);
            }
        }
    }
}
