using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlMe
{
    public partial class Upload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            filefull.Text = Request.QueryString["file"];
        }

        protected void UploadButton_Click(object sender, EventArgs e)
        {
            if (FileUpload1.HasFile)
            {
                try
                {                    
                    string filename = Path.GetFileName(FileUpload1.FileName);
                    string savePath = Server.MapPath("~/Storage/") + filename;
                    FileUpload1.SaveAs(savePath);
                    StatusLabel.Text = "File uploaded successfully! Please close this window.";
                }
                catch (Exception ex)
                {
                    StatusLabel.Text = "File upload failed: " + ex.Message;
                }
            }
            else
            {
                StatusLabel.Text = "Please select a file to upload.";
            }
        }
    }
}