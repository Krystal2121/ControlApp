using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SqlHelper;
using System.Configuration;
using System.Data;
using ControlMe.Pages.Sub;
using System.Web.Caching;

namespace ControlMe.Pages
{
    public partial class SubPage : System.Web.UI.Page
    {
        SqlHelper.SqlHelp shelp;
        public string SID;
        public string DID;
        protected void Page_Load(object sender, EventArgs e)
        {
            string val = "UserID";
            HttpCookie myCookie = HttpContext.Current.Request.Cookies[val];
            SID = myCookie.Value;
            SqlHelper.SqlHelp shelp = new SqlHelp();
            string ConnectionStr = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Environ"].ToString()].ConnectionString; ;
            string errorstr;
            string commandstr = "";
            DataSet retdata;
            if (!IsPostBack)
            {
                commandstr = "exec [dbo].[USP_GetDom] " + SID;
                retdata = shelp.DatasetRet(commandstr, ConnectionStr, out errorstr);
                if (retdata.Tables.Count > 0 && retdata.Tables[0].Rows.Count > 0)
                {
                    doms.DataSource = retdata;
                    doms.DataTextField = "UserName"; // The column to display in the dropdown
                    doms.DataValueField = "DomId"; // The column to use as the value
                    doms.DataBind();
                }
            }
            commandstr = "exec [dbo].[Usp_GetInvites] " + SID;
            retdata = shelp.DatasetRet(commandstr, ConnectionStr, out errorstr);
            if (errorstr != "")
            {
                ErrorLb.Text = errorstr;
                ErrorLb.Visible = true;
            }
            else
            {
                if (retdata.Tables[0].Rows.Count > 0)
                {
                    Invites.Visible = true;
                    DataTable dt = retdata.Tables[0];
                    DataGrid1.DataSource = dt; // dataset
                    DataGrid1.DataBind();
                }else
                {
                    Invites.Visible = false;
                }
            }
            LoadChat();
        }
        public void LoadChat()
        {
            try
            {
                ChatLog.Text = "";
                SqlHelp shelp = new SqlHelp();
                string ConnectionStr = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Environ"].ToString()].ConnectionString;
                string errorstr = "";
                string commandstr = "";
                commandstr = "exec [dbo].[USP_GetChatLog] " + SID + "," + doms.SelectedItem.Value.ToString();
                DataSet ds = shelp.DatasetRet(commandstr, ConnectionStr, out errorstr);
                if (errorstr != "")
                { ErrorLb.Text = errorstr; }
                else
                {
                    int counting = ds.Tables[0].Rows.Count;
                    for (int x = 0; x < counting; x++)
                    {
                        string type = ds.Tables[0].Rows[x][0].ToString();
                        string chattext = ds.Tables[0].Rows[x][1].ToString();
                        if (type == "R")
                        {
                            ChatLog.Text += doms.SelectedItem.Text + " : " + chattext;
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
            }
            catch
            {
                ErrorLb.Text = "Error loading chat";
                ErrorLb.Visible = true;
            }
        }
        protected void doms_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadChat();
        }
        protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            DID = e.Item.Cells[0].Text;
            string errorstr = "";
            if (e.CommandName == "Accept")
            {
                SqlHelper.SqlHelp shelp = new SqlHelp();
                string ConnectionStr = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Environ"].ToString()].ConnectionString;
                string commandstr = "";
                commandstr = "exec usp_AcceptInvite " + SID + ",'" + DID + "'";
                string rows = shelp.RowsAffect(commandstr, ConnectionStr, out errorstr).ToString();
            }
            else
                if (e.CommandName == "Reject")
            {
                SqlHelper.SqlHelp shelp = new SqlHelp();
                string ConnectionStr = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Environ"].ToString()].ConnectionString;
                string commandstr = "";
                commandstr = "exec usp_DeleteInvite " + SID + ",'" + DID + "'";
                string rows = shelp.RowsAffect(commandstr, ConnectionStr, out errorstr).ToString();
            }
            if (errorstr != "")
            {
                ErrorLb.Text = errorstr;
                ErrorLb.Visible = true;
            }
            else
            {
                Response.Redirect("~/Pages/Sub/SubPage.aspx");
            }
        }

        protected void sendmsgbtn_Click(object sender, EventArgs e)
        {
            SqlHelper.SqlHelp shelp = new SqlHelp();
            string ConnectionStr = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Environ"].ToString()].ConnectionString;
            string errorstr;
            string commandstr = "";
            commandstr = "exec usp_SendChat " + SID + "," + doms.SelectedValue.ToString() + ",'" + sendmsg.Text + "'";
            shelp.NoReturn(commandstr, ConnectionStr, out errorstr);
            if (errorstr != "")
            {
                ErrorLb.Text = errorstr;
                ErrorLb.Visible = true;
            }
            else
            {
                sendmsg.Text = "";
                LoadChat();
            }
        }
    }
}