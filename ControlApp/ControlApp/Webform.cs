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
    public partial class Webform : Form
    {
        public Webform(string Url)
        {
            WebBrowser wb = new WebBrowser();
            {
                wb.Navigate(Url);
                wb.Dock = DockStyle.Fill;
            }
            Controls.Add(wb);
            InitializeComponent();
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
        }
    }

}
