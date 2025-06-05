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
    public partial class WriteForMe : Form
    {
        int seconds;
        int mistakes;
        int count;
        string senderstr;
        public WriteForMe(string message, string times, string senderid)
        {
            InitializeComponent();
            writelbl.Text = message;
            count = Convert.ToInt16(times);
            countlbl.Text = times;
            senderstr = senderid;
        }

        private void WriteForMe_Load(object sender, EventArgs e)
        {
            seconds = 0;
            mistakes = 0;
            mistakelbl.Text = mistakes.ToString();
            timelbl.Text = seconds.ToString();
            timer1.Interval = (1000); // 45 mins
            timer1.Tick += new EventHandler(MyTimer_Tick);
            timer1.Start();
        }
        private void MyTimer_Tick(object sender, EventArgs e)
        {
            seconds++;
        }
        private void input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {                
                if (wfmtextb.Text == writelbl.Text)
                {
                    count--;
                    countlbl.Text = count.ToString();
                    timelbl.Text = seconds.ToString();
                }
                else
                {
                    mistakes++;
                    mistakelbl.Text = mistakes.ToString();
                    timelbl.Text = seconds.ToString();
                }
                wfmtextb.Text = "";
                if (count == 0)
                {
                    Utils ut = new Utils();
                    string usrnm = ConfigurationManager.AppSettings["UserName"].ToString();
                    ut.sendcmd(senderstr, ut.Ecrypt("M=" + usrnm + " completed your command in " + timelbl.Text + " seconds with " + mistakelbl.Text + " mistakes.&&&Please Reward"), false);
                    this.Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Utils ut = new Utils();
            string usrnm = ConfigurationManager.AppSettings["UserName"].ToString();
            ut.sendcmd(senderstr, ut.Ecrypt("M="+ usrnm + " failed your command after " + timelbl.Text + " seconds with " + mistakelbl.Text + " mistakes.&&&Please Punish"), false);
            this.Close();
        }
    }
}
