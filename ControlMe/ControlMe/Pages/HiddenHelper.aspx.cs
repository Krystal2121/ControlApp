using SqlHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlMe.Pages
{
    public partial class HiddenHelper : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlHelp shelp = new SqlHelp();
            string usernm = Request.QueryString["user"];
            string password = Request.QueryString["password"];
            string ConnectionStr = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Environ"].ToString()].ConnectionString;
            string errorstr = "";
            string commandstr = "";
            commandstr = "exec [dbo].[Usp_GetVerificationReq]";
            DataSet ds = shelp.DatasetRet(commandstr, ConnectionStr, out errorstr);
            if (errorstr != "")
            { Errorstrlbl.Text = errorstr; }
            else
            {
                DataTable dt = ds.Tables[0];
                DataGrid2.DataSource = dt; // dataset
                DataGrid2.DataBind();
            }
        }
    }
}