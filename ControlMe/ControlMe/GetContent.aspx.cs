using SqlHelper;
using CryptoHelp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlMe
{
    public partial class GetContent : System.Web.UI.Page
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
                if (retdata.Tables[0].Rows.Count > 0)
                {
                    try
                    {
                        string ID = retdata.Tables[0].Rows[0][0].ToString();
                        commandstr = "exec [dbo].[USP_GetAppContent] " + ID;
                        DataSet retdata2 = shelp.DatasetRet(commandstr, ConnectionStr, out errorstr);
                        if (errorstr != "")
                        {
                            Result.Text = errorstr;
                        }
                        else
                        {
                            if (retdata2.Tables[0].Rows.Count > 0)
                            {
                                SenderId.Text = retdata2.Tables[0].Rows[0][0].ToString();
                                Result.Text = retdata2.Tables[0].Rows[0][1].ToString();                                
                            }
                        }
                        commandstr = "exec [dbo].[USP_CmdComplete] " + ID;
                        shelp.NoReturn(commandstr, ConnectionStr, out errorstr);
                    }
                    catch { }
                }
            }
        }
    }
}