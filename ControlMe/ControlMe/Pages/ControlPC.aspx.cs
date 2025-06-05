using SqlHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlMe.Pages
{
    public partial class ControlPC : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UserNm.Text = Request.QueryString["user"];
                errorlbl.Text = "";
            }
        }

        protected void Send_Click(object sender, EventArgs e)
        {
            SqlHelper.SqlHelp shelp = new SqlHelp();
            CryptoHelp.CryptoHelper cryhlp = new CryptoHelp.CryptoHelper();
            string Content = "";
            if (MessBx.Text != "")
            {
                Content += cryhlp.Ecrypt("M=" + MessBx.Text + "&&&" + Btnbx.Text) + "|||";
            }
            if (DownLd.Text != "")
            {
                Content += cryhlp.Ecrypt("D=" + DownLd.Text) + "|||";
            }
            if (wallp.Text != "")
            {
                Content += cryhlp.Ecrypt("P=" + wallp.Text) + "|||";
            }
            if (runf.Text != "")
            {
                Content += cryhlp.Ecrypt("R=" + runf.Text) + "|||";
            }
            if (openw.Text != "")
            {
                if (!openw.Text.Contains("booru.allthefallen.moe"))
                    Content += cryhlp.Ecrypt("W=" + openw.Text) + "|||";
            }
            if (popd.Text != "")
            {
                Content += cryhlp.Ecrypt("U=" + popd.Text) + "|||";
            }
            if (popu.Text != "")
            {
                Content += cryhlp.Ecrypt("O=" + popu.Text) + "|||";
            }
            if (twitterTxt.Text != "")
            {
                string temp = twitterTxt.Text.Replace(" ", "%20");
                Content += cryhlp.Ecrypt("3=" + twitterTxt.Text) + "|||";
            }
            if (Sublimbx.Text != "")
            {
                if (isurl.Checked)
                {
                    Content += cryhlp.Ecrypt("S=" + Sublimbx.Text) + "|||";
                }
                else
                {
                    Content += cryhlp.Ecrypt("V=" + Sublimbx.Text) + "|||";
                }
            }
            string ConnectionStr = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Environ"].ToString()].ConnectionString; ;
            string errorstr;
            string commandstr = "";
            string all = "0";
            if (SndAll.Checked)
            {
                all = "1";
            }
            if (SendUsr.Text != "")
            {
                commandstr = "exec usp_SendAppCmd '" + UserNm.Text + "','" + Content + "'," + all + ",'" + SendUsr.Text + "','" + SendPwd.Text + "'";
                shelp.NoReturn(commandstr, ConnectionStr, out errorstr);
                if (errorstr.Length > 0)
                {
                    errorlbl.Text = errorstr;
                }
                else
                {
                    Response.Redirect("ControlPC.aspx?user=" + UserNm.Text);
                }
            }
            else
            { errorlbl.Text = "Must enter user information."; }
        }
    }
}