using ControlMe.Pages.Doms;
using ControlMe.Pages.Sub;
using SqlHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlMe
{
    public partial class Messages : System.Web.UI.Page
    {
        string ID;
        string other;
        protected void Page_Load(object sender, EventArgs e)
        {
            CryptoHelp.CryptoHelper ch = new CryptoHelp.CryptoHelper();
            string usernm = Request.QueryString["usernm"];
            string pwd = ch.Decrypt(Request.QueryString["pwd"]);
            other = Request.QueryString["othr"];
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
                        Message.Text = errorstr;
                    }
                    else
                    if ((retdata.Tables.Count > 0) && (retdata.Tables[0].Rows.Count > 0))
                    {
                        ID = retdata.Tables[0].Rows[0][0].ToString();
                    }
                    LoadChat();
                }
                catch { }
            }

        }
        public void LoadChat()
        {
            Message.Text = "";
            SqlHelp shelp = new SqlHelp();
            string ConnectionStr = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Environ"].ToString()].ConnectionString;
            string errorstr = "";
            string commandstr = "";
            commandstr = "exec [dbo].[USP_GetChatLog] " + ID + ",0,'" + other + "'";
            DataSet ds = shelp.DatasetRet(commandstr, ConnectionStr, out errorstr);
            int counting = ds.Tables[0].Rows.Count;
            for (int x = 0; x < counting; x++)
            {
                string type = ds.Tables[0].Rows[x][0].ToString();
                string chattext = ds.Tables[0].Rows[x][1].ToString();
                if (type == "R")
                {
                    Message.Text += other + " : " + chattext;
                    Message.Text += Environment.NewLine;
                }
                else
                    if (type == "S")
                {
                    Message.Text += "You : " + chattext;
                    Message.Text += Environment.NewLine;
                }

            }
        }

        protected void Sendmsgbtn_Click(object sender, EventArgs e)
        {
            SqlHelper.SqlHelp shelp = new SqlHelp();
            string ConnectionStr = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Environ"].ToString()].ConnectionString;
            string errorstr;
            string commandstr = "";
            commandstr = "exec usp_SendChat " + ID + ",0,'" + sndmsgtxt.Text + "','" + other + "'";
            shelp.NoReturn(commandstr, ConnectionStr, out errorstr);
            LoadChat();
        }
    }
}