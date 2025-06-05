using ControlMe.Pages.Doms;
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

namespace ControlMe.Pages
{
    public partial class DomPage : System.Web.UI.Page
    {
        string ID;
        protected void Page_Load(object sender, EventArgs e)
        {
            Errorlbl.Text = "";
            SqlHelp shelp = new SqlHelp();
            string val = "UserID";
            HttpCookie myCookie = HttpContext.Current.Request.Cookies[val];
            ID = myCookie.Value;
            if (!IsPostBack)            {

                string ConnectionStr = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Environ"].ToString()].ConnectionString;
                string errorstr = "";
                string commandstr = "";
                commandstr = "exec [dbo].[USP_GetSub] " + ID;
                DataSet ds = shelp.DatasetRet(commandstr, ConnectionStr, out errorstr);
                subs.DataSource = ds;
                subs.DataTextField = "UserName"; // The column to display in the dropdown
                subs.DataValueField = "SubId"; // The column to use as the value
                subs.DataBind();
            }
            LoadChat();
        }
        public void LoadChat()
        {
            try { 
            ChatLog.Text = "";
            SqlHelp shelp = new SqlHelp();
            string ConnectionStr = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Environ"].ToString()].ConnectionString;
            string errorstr = "";
            string commandstr = "";
            commandstr = "exec [dbo].[USP_GetChatLog] " + ID + "," + subs.SelectedItem.Value.ToString();
            DataSet ds = shelp.DatasetRet(commandstr, ConnectionStr, out errorstr);
            if (errorstr != "")
            { Errorlbl.Text = errorstr; }
            else
            {
                int counting = ds.Tables[0].Rows.Count;
                for (int x = 0; x < counting; x++)
                {
                    string type = ds.Tables[0].Rows[x][0].ToString();
                    string chattext = ds.Tables[0].Rows[x][1].ToString();
                    if (type == "R")
                    {
                        ChatLog.Text += subs.SelectedItem.Text + " : " + chattext;
                        ChatLog.Text += Environment.NewLine;
                    }
                    else
                        if (type == "S")
                    {
                        ChatLog.Text += "You : " + chattext;
                        ChatLog.Text += Environment.NewLine;
                    }

                }
            }
        }catch
            { Errorlbl.Text = "Error loading chat"; }
}
        protected void subs_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadChat();
        }

        protected void senInviteBtn_Click(object sender, EventArgs e)
        {
            SqlHelp shelp = new SqlHelp();
            string ConnectionStr = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Environ"].ToString()].ConnectionString;
            string errorstr = "";
            string commandstr = "";
            commandstr = "exec [dbo].[USP_AddInvite] " + ID + ",'" + InviteSub.Text + "'";
            shelp.NoReturn(commandstr, ConnectionStr, out errorstr);
            if (errorstr!="")
            {
                Errorlbl.Text = errorstr;
            }else
            {
                Errorlbl.Text = "Invite sent to " + InviteSub.Text;
            }
        }

        protected void sendmsgbtn_Click(object sender, EventArgs e)
        {
            SqlHelper.SqlHelp shelp = new SqlHelp();
            string ConnectionStr = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Environ"].ToString()].ConnectionString;
            string errorstr;
            string commandstr = "";
            commandstr = "exec usp_SendChat " + ID + "," + subs.SelectedValue.ToString() + ",'" + sendmsg.Text + "'";
            shelp.NoReturn(commandstr, ConnectionStr, out errorstr);
        }
    }
}