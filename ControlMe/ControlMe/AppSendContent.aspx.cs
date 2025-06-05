using SqlHelper;
using CryptoHelp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

namespace ControlMe
{
    public partial class AppSendContent : System.Web.UI.Page
    {
        private static bool IsLocalPath(string p)
        {
            return new Uri(p).IsFile;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                CryptoHelp.CryptoHelper cryptoHelper = new CryptoHelper();
                string usernm = Request.QueryString["usernm"];
                string comm = Request.QueryString["comm"].Replace("PPP", "G0");
                string all = Request.QueryString["all"];
                string fromuser = Request.QueryString["fromuser"];
                string frompword = cryptoHelper.Decrypt(Request.QueryString["frompword"].Replace("PPP","G0"));

                SqlHelper.SqlHelp shelp = new SqlHelp();
                string ConnectionStr = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Environ"].ToString()].ConnectionString; ;
                string errorstr;
                string commandstr = "";
                commandstr = "exec usp_SendAppCmd '" + usernm + "','" + comm + "', " + all + ",'" + fromuser + "', '" + frompword + "'";
                string rows = shelp.RowsAffect(commandstr, ConnectionStr, out errorstr).ToString();

                if (errorstr.Length > 0)
                {
                    errorlbl.Text = errorstr;
                }
                else
                {
                    Response.Redirect("ProcessComplete.aspx");
                }
            }
            catch (Exception ex)
            {
                errorlbl.Text = ex.Message;
            }
        }
    }
}