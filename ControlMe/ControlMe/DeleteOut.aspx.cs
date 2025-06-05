using SqlHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Net.Mime.MediaTypeNames;

namespace ControlMe
{
    public partial class DeleteOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CryptoHelp.CryptoHelper ch = new CryptoHelp.CryptoHelper();
            string usernm = Request.QueryString["usernm"];
            string pwd = ch.Decrypt(Request.QueryString["pwd"]);
            string version = Request.QueryString["vrs"];
            if (version == "012")
            {
                SqlHelper.SqlHelp shelp = new SqlHelp();
                string ConnectionStr = "";
                ConnectionStr = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Environ"].ToString()].ConnectionString;
                string errorstr;
                string commandstr = "";
                commandstr = "exec usp_login '" + usernm + "','" + pwd + "'";
                DataSet retdata = shelp.DatasetRet(commandstr, ConnectionStr, out errorstr);
                if (errorstr != "")
                {
                    result.Text = errorstr;
                }
                else
                if ((retdata.Tables.Count > 0) && (retdata.Tables[0].Rows.Count > 0))
                {
                    string ID = retdata.Tables[0].Rows[0][0].ToString();
                    commandstr = "exec [dbo].[USP_DeleteOutstanding] " + ID;
                    DataSet data = shelp.DatasetRet(commandstr, ConnectionStr, out errorstr);
                    if (errorstr != "")
                    {
                        result.Text = errorstr;
                    }
                }
            }
        }
    }
}