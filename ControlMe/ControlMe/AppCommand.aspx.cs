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
    public partial class AppCommand : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CryptoHelp.CryptoHelper ch = new CryptoHelp.CryptoHelper();
            string usernm = Request.QueryString["usernm"];
            string pwd = ch.Decrypt(Request.QueryString["pwd"]);
            //string pwd = Request.QueryString["pwd"];
            string version = Request.QueryString["vrs"];
            string command = Request.QueryString["cmd"];
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
                        if (command == "Outstanding")
                        {
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
                                    result.Text = "[" + data.Tables[0].Rows[0][0].ToString() + "],[" + data.Tables[0].Rows[0][1].ToString() + "],[" + data.Tables[0].Rows[0][2].ToString() + "],[" + data.Tables[0].Rows[0][3].ToString() + "]";
                                }
                            }
                        }
                        else
                            if (command == "Content")
                        {
                            commandstr = "exec [dbo].[USP_GetAppContent] " + ID;
                            DataSet retdata2 = shelp.DatasetRet(commandstr, ConnectionStr, out errorstr);
                            if (errorstr != "")
                            {
                                result.Text = errorstr;
                            }
                            else
                            {
                                if (retdata2.Tables[0].Rows.Count > 0)
                                {
                                    if (retdata2.Tables[0].Columns.Count > 3)
                                        result.Text = "[" + retdata2.Tables[0].Rows[0][0].ToString() + "],[" + retdata2.Tables[0].Rows[0][1].ToString() + "],[" + retdata2.Tables[0].Rows[0][2].ToString() + "]";
                                    else
                                        result.Text = "[" + retdata2.Tables[0].Rows[0][0].ToString() + "],[" + retdata2.Tables[0].Rows[0][1].ToString() + "]";
                                }   
                            }
                            commandstr = "exec [dbo].[USP_CmdComplete] " + ID;
                            shelp.NoReturn(commandstr, ConnectionStr, out errorstr);
                        }
                        else
                            if (command == "Delete")
                        {
                            commandstr = "exec [dbo].[USP_DeleteOutstanding] " + ID;
                            DataSet data = shelp.DatasetRet(commandstr, ConnectionStr, out errorstr);
                            if (errorstr != "")
                            {
                                result.Text = errorstr;
                            }
                        }
                        else if (command == "Invite")
                        {
                            commandstr = "exec [dbo].[USP_Getinvites2] " + ID;
                            result.Text = shelp.StringRet(commandstr, ConnectionStr, out errorstr);
                            if (errorstr != "")
                            {
                                result.Text = errorstr;
                            }
                        }
                        else if (command == "Relations")
                        {
                            commandstr = "exec [dbo].[USP_GetRels] " + ID;
                            result.Text = shelp.StringRet(commandstr, ConnectionStr, out errorstr);
                            if (errorstr != "")
                            {
                                result.Text = errorstr;
                            }
                        }
                        else if (command.Substring(0,6) == "Accept")
                        {
                            commandstr = "exec [dbo].[USP_AcceptInvite] " + ID + ",'" + command.Substring(6, command.Length - 6) + "'";
                            shelp.NoReturn(commandstr, ConnectionStr, out errorstr);
                            if (errorstr != "")
                            {
                                result.Text = errorstr;
                            }
                        }
                        else if (command.Substring(0, 6) == "Reject")
                        {
                            commandstr = "exec [dbo].[USP_DeleteInvite] " + ID + ",'" + command.Substring(6, command.Length - 6) + "'";
                            shelp.NoReturn(commandstr, ConnectionStr, out errorstr);
                            if (errorstr != "")
                            {
                                result.Text = errorstr;
                            }
                        }
                        else if (command.Substring(0, 6) == "Thumbs")
                        {
                            commandstr = "exec [dbo].[USP_thumbsup] " + ID + "," + command.Substring(6, command.Length - 6);
                            shelp.NoReturn(commandstr, ConnectionStr, out errorstr);
                            if (errorstr != "")
                            {
                                result.Text = errorstr;
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