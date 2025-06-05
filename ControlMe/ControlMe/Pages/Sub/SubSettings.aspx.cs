using SqlHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlMe.Pages.Sub
{
    public partial class SubSettings : System.Web.UI.Page
    {
        string ID;
        SqlHelper.SqlHelp shelp;
        string optin;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                shelp = new SqlHelp();
                string usernm = Request.QueryString["user"];
                string password = Request.QueryString["password"];
                string ConnectionStr = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Environ"].ToString()].ConnectionString;
                string errorstr = "";
                string commandstr = "";
                if ((usernm != null) && (usernm != ""))
                {
                    commandstr = "exec usp_login '" + usernm + "','" + password + "'";
                    DataSet retdata = shelp.DatasetRet(commandstr, ConnectionStr, out errorstr);
                    if (errorstr != "")
                    { errorsstrlbl.Text = errorstr; }
                    else
                    if ((retdata != null) && (retdata.Tables.Count > 0) && (retdata.Tables[0].Rows.Count > 0))
                    {
                        ID = retdata.Tables[0].Rows[0][0].ToString();
                        HttpCookie myCookie = new HttpCookie("UserID");
                        myCookie.Value = ID;
                        myCookie.Expires = DateTime.Now.AddDays(1d);
                        Response.Cookies.Add(myCookie);
                    }
                }
                else
                {
                    string val = "UserID";
                    HttpCookie myCookie = HttpContext.Current.Request.Cookies[val];
                    ID = myCookie.Value;

                }
                commandstr = "exec [dbo].[USP_GetUserSettings] '" + ID + "'";
                DataSet ds = shelp.DatasetRet(commandstr, ConnectionStr, out errorstr);
                if (errorstr != "")
                { errorsstrlbl.Text = errorstr; }
                else
                if (ds != null)
                {
                    if (ds.Tables[0].Rows[0]["RandOpt"].ToString() == "True")
                    {
                        optinchk.Checked = true;
                    }else
                    { optinchk.Checked = false; }


                    if (ds.Tables[0].Rows[0]["AnonCmd"].ToString() == "True")
                    { 
                        anonchk.Checked = true; 
                    }else { anonchk.Checked = false; }


                    passwordtxt.Text = ds.Tables[0].Rows[0]["Password"].ToString();

                    emailtxt.Text=ds.Tables[0].Rows[0]["Login Name"].ToString();

                }
                
            }
        }

        protected void SaveBtn_Click(object sender, EventArgs e)
        {

            string val = "UserID";
            HttpCookie myCookie = HttpContext.Current.Request.Cookies[val];
            ID = myCookie.Value;

            shelp = new SqlHelp();
            string ConnectionStr = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Environ"].ToString()].ConnectionString; 
            string errorstr = "";
            string commandstr = "";
            string optin = "0";
            string anon = "0";
            if (optinchk.Checked == true)
                optin = "1";
            if (anonchk.Checked == true)
                anon = "1";
            commandstr = "exec [dbo].[USP_UpdateSettings] " + ID + ", " + optin + ",'" + passwordtxt.Text + "'," + anon+", '"+emailtxt.Text+"'";
            shelp.NoReturn(commandstr, ConnectionStr, out errorstr);
            if (errorstr != "")
            { errorsstrlbl.Text = errorstr; }
            Response.Redirect("SubSettings.aspx");
        }

    }
}