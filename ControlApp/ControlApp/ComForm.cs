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
    public partial class ComForm : Form
    {
        public string[] ReturnedData { get; private set; }
        private string formtype;
        public ComForm(string Url,string type)
        {
            formtype = type;
            ReturnedData = new string[]{ "","",""};
            WebBrowser wb = new WebBrowser();
            {
                wb.Name = "TheWebBrowser";
                wb.Navigate(Url);
                wb.Dock = DockStyle.Fill;
                wb.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(TheWebBrowser_DocumentCompleted);
            }
            Controls.Add(wb);
            InitializeComponent();
        }

        private void ComForm_Load(object sender, EventArgs e)
        {

        }
        private void TheWebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (this.Controls["TheWebBrowser"] != null)
            {
                
                WebBrowser myControl = (WebBrowser)this.Controls["TheWebBrowser"];
                string htmlContent = myControl.DocumentText;
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(htmlContent);
                var labelNode = doc.GetElementbyId("result");
                if (labelNode != null)
                {
                    // Display the label's text in a Windows Forms label control
                    ReturnedData[0] = labelNode.InnerText;
                }
                if (formtype == "CheckNext")
                {
                    labelNode = doc.GetElementbyId("next");
                    if (labelNode != null)
                    {
                        // Display the label's text in a Windows Forms label control
                        ReturnedData[1] = labelNode.InnerText;
                    }
                    labelNode = doc.GetElementbyId("vari");
                    if (labelNode != null)
                    {
                        // Display the label's text in a Windows Forms label control
                        ReturnedData[2] = labelNode.InnerText;
                    }
                }
            }
            this.Close();
        }
    }
}
