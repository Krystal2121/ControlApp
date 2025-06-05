using SqlHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlMe.Pages.Sub
{
    public partial class SubMain : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string val = "UserID";
            HttpCookie myCookie = HttpContext.Current.Request.Cookies[val];
            ID = myCookie.Value;
            SqlHelper.SqlHelp shelp = new SqlHelp();
            string ConnectionStr = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Environ"].ToString()].ConnectionString; ;
            string errorstr;
            string commandstr = "";
            commandstr = "exec [dbo].[Usp_checkuser]" + ID;
            string retdata = shelp.StringRet(commandstr, ConnectionStr, out errorstr);
            if (retdata == "0")
                NotVar.Visible = true;
            else if (retdata == "-1")
                VarReq.Visible = true;
            commandstr = "exec [dbo].[usp_bestsender] ";
            try
            {
                DataSet ds = shelp.DatasetRet(commandstr, ConnectionStr, out errorstr);
                if (errorstr != "")
                { Errorlbl.Text = errorstr; }
                else
                {
                    DataTable dt = ds.Tables[0];
                    Scores.DataSource = dt; // dataset
                    Scores.DataBind();
                }
            } catch {
                Errorlbl.Text = "Error loading thumbs up data.";
                    }
        }

        protected void VarifyBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string val = "UserID";
                HttpCookie myCookie = HttpContext.Current.Request.Cookies[val];
                ID = myCookie.Value;
                SqlHelper.SqlHelp shelp = new SqlHelp();
                string ConnectionStr = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Environ"].ToString()].ConnectionString; 
                string errorstr;
                string commandstr = "";
                commandstr = "exec [dbo].[usp_requestvar]" + ID;
                DataSet retdata = shelp.DatasetRet(commandstr, ConnectionStr, out errorstr);
                NotVar.Visible = false;
            }
            catch (Exception ex)
            {
                Errorlbl.Text = ex.Message;
            }
        }

        protected void entrbtn_Click(object sender, EventArgs e)
        {
            try
            {
                string val = "UserID";
                HttpCookie myCookie = HttpContext.Current.Request.Cookies[val];
                ID = myCookie.Value;
                SqlHelper.SqlHelp shelp = new SqlHelp();
                string ConnectionStr = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Environ"].ToString()].ConnectionString; ;
                string errorstr;
                string commandstr = "";
                commandstr = "exec [dbo].[usp_varify]" + ID + "," + VarCode.Text;
                string retdata = shelp.StringRet(commandstr, ConnectionStr, out errorstr);
                VarCode.Text = retdata;
                if (retdata == "Done")
                {
                    VarReq.Visible = false;
                }
            }
            catch { }
        }
    }
}