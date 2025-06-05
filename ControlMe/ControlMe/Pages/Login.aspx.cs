using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SqlHelper;
using System.Configuration;
using System.Data;
using System.Net;

namespace ControlMe.Pages
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SubmitBtn_Click(object sender, EventArgs e)
        {
            SqlHelper.SqlHelp shelp = new SqlHelp();
            string ConnectionStr = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Environ"].ToString()].ConnectionString;;
            string errorstr;
            string commandstr = "";
            commandstr = "exec usp_login '" + UserNameTxt.Text + "','" + PasswordTxt.Text + "'";
            DataSet retdata= shelp.DatasetRet(commandstr, ConnectionStr, out errorstr);
            if(errorstr!="")
            { errorstrlbl.Text = errorstr; }
            else
            if ((retdata!=null)&&(retdata.Tables[0].Rows.Count > 0))
            {
                string ID = retdata.Tables[0].Rows[0][0].ToString();
                string Role = retdata.Tables[0].Rows[0][1].ToString();
                string varified = retdata.Tables[0].Rows[0][2].ToString();
                HttpCookie myCookie = new HttpCookie("UserID");
                myCookie.Value = ID;
                myCookie.Expires = DateTime.Now.AddDays(1d);
                Response.Cookies.Add(myCookie);
                if (Role == "Sub")
                    Response.Redirect("Sub/SubMain.aspx");
                else
                    Response.Redirect("Dom/DomMain.aspx");
            }
            else
            { }
        }
    }
}