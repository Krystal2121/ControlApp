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
    public partial class Other : Form
    {
        public Other()
        {
            InitializeComponent();
        }

        private void svncls_Click(object sender, EventArgs e)
        {
            Configuration myconfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            KeyValueConfigurationCollection apps = myconfig.AppSettings.Settings;
            apps.Remove("CommonUsers");
            string userlist = "";
            foreach (string line in textBox1.Lines)
            {
                if (userlist == "")
                    userlist += "[" + line + "]";
                else
                    userlist += ",[" + line + "]";
            }
            apps.Add("CommonUsers", userlist);

            apps.Remove("BlackList");
            string blist = "";
            foreach (string line in textBox2.Lines)
            {
                if (blist == "")
                    blist += "[" + line + "]";
                else
                    blist += ",[" + line + "]";
            }
            apps.Add("BlackList", blist);

            apps.Remove("UserBList");
            string Ublist = "";
            foreach (string line in textBox3.Lines)
            {
                if (Ublist == "")
                    Ublist += "[" + line + "]";
                else
                    Ublist += ",[" + line + "]";
            }
            apps.Add("UserBList", Ublist);
            myconfig.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection(myconfig.AppSettings.SectionInformation.Name);
            this.Close();
        }

        private void Other_Load(object sender, EventArgs e)
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
            string userlist = ConfigurationManager.AppSettings["CommonUsers"];
            string blist = ConfigurationManager.AppSettings["BlackList"];
            string Ublist = ConfigurationManager.AppSettings["UserBList"];
            Utils utils = new Utils(); ;
            if (userlist != null)
            {
                string[] users = utils.seperate_string(userlist);
                textBox1.Lines = users;
            }
            if (blist != null)
            {
                string[] blists = utils.seperate_string(blist);
                textBox2.Lines = blists;
            }
            if (Ublist != null)
            {                
                string[] blists = utils.seperate_string(Ublist);
                textBox3.Lines = blists;
            }
        }
    }
}
