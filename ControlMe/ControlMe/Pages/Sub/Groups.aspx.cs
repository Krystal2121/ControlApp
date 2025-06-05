using CryptoHelp;
using SqlHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlMe.Pages.Sub
{
    public partial class Groups : System.Web.UI.Page
    {
        string ID;
        protected void Page_Load(object sender, EventArgs e)
        {
            ID = "";
            SqlHelp shelp = new SqlHelp();
            string ConnectionStr = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Environ"].ToString()].ConnectionString;
            string errorstr = "";
            string commandstr = "";
            string val = "UserID";
            HttpCookie myCookie = HttpContext.Current.Request.Cookies[val];
            ID = myCookie.Value;

            commandstr = "exec USP_GetGroups " + ID;
            DataSet ds = shelp.DatasetRet(commandstr, ConnectionStr, out errorstr);
            if (errorstr != "")
            {
                Errorstrlbl.Text = errorstr;
            }
            else
            {

                DataGrid1.DataSource = ds; // dataset
                DataGrid1.DataBind();
            }
        }

        protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            { // Get the selected item's data
              //
              // Perform your logic here
                string group = e.Item.Cells[0].Text;

                SqlHelper.SqlHelp shelp = new SqlHelp();
                string ConnectionStr = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Environ"].ToString()].ConnectionString;
                string errorstr;
                string commandstr = "";
                commandstr = "exec [dbo].[USP_AddGroup] " + ID + ",'" + group + "'";
                string rows = shelp.RowsAffect(commandstr, ConnectionStr, out errorstr).ToString();
                Response.Redirect("Groups.aspx");
            }
        }
    }
}