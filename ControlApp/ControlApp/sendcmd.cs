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



namespace ControlApp
{
    public partial class sendcmd : Form
    {

        public sendcmd(string Url)
        {
            WebBrowser wb = new WebBrowser();
            {
                wb.Navigate(Url);
                wb.Dock = DockStyle.Fill;
            }
            Controls.Add(wb);
            InitializeComponent();
        }

        private async void sendcmd_Load(object sender, EventArgs e)
        {
            SendToBack();
            await Task.Delay(3000);
            this.Close();
        }
    }
}
