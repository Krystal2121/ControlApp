using SqlHelper;
using CryptoHelp;
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
    public partial class GetCount : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CryptoHelp.CryptoHelper ch = new CryptoHelp.CryptoHelper();
            string usernm = Request.QueryString["usernm"];
            string pwd = ch.Decrypt(Request.QueryString["pwd"]);
            string version = Request.QueryString["vrs"];
            if (version == "012") 
            {
                try
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
                        commandstr = "exec [dbo].[USP_GetOutstanding] " + ID;
                        DataSet data = shelp.DatasetRet(commandstr, ConnectionStr, out errorstr);
                        if (errorstr != "")
                        {
                            result.Text = errorstr;
                        }
                        else
                        {
                            if ((data.Tables.Count > 0) && (data.Tables[0].Rows.Count > 0))
                            {
                                result.Text = data.Tables[0].Rows[0][0].ToString();
                                next.Text = data.Tables[0].Rows[0][1].ToString();
                                vari.Text = data.Tables[0].Rows[0][2].ToString();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.Text = ex.Message;
                }
            }
        }
    }
}