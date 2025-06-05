using SqlHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlMe
{
    public partial class BlockReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CryptoHelp.CryptoHelper ch = new CryptoHelp.CryptoHelper();
            string usernm = Request.QueryString["usernm"];
            string pwd = ch.Decrypt(Request.QueryString["pwd"]);
            string version = Request.QueryString["vrs"];
            string senderid = Request.QueryString["sender"];
            string report = Request.QueryString["report"];
            string content = Request.QueryString["content"];
            if (version == "012")
            {
                SqlHelper.SqlHelp shelp = new SqlHelp();
                string ConnectionStr = "";
                ConnectionStr = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Environ"].ToString()].ConnectionString;
                string errorstr;
                string commandstr = "";
                commandstr = "exec usp_login '" + usernm + "','" + pwd + "'";
                DataSet retdata = shelp.DatasetRet(commandstr, ConnectionStr, out errorstr);
                if (retdata.Tables[0].Rows.Count > 0)
                {
                    string ID = retdata.Tables[0].Rows[0][0].ToString();
                    if (report == "1")
                    { commandstr = "exec [dbo].[USP_Report] " + ID + "," + senderid + ",'" + content + "'"; }
                    else
                    {
                        commandstr = "exec [dbo].[USP_BlockUser] " + ID + "," + senderid;
                    }
                    shelp.NoReturn(commandstr, ConnectionStr, out errorstr);
                    if (errorstr != "")
                    {
                        result.Text = errorstr;
                    }
                    else
                    {
                        Response.Redirect("default.aspx");
                    }
                }
            }else
            {
                result.Text = "Wrong version.";
            }
        }
    }
}