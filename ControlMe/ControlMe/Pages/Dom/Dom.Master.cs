using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlMe.Pages.Doms
{
    public partial class Dom : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string ID = "-1";
            string val = "UserID";
            HttpCookie myCookie = HttpContext.Current.Request.Cookies[val];
            ID = myCookie.Value;
            if (ID == null || ID == "-1")
            {
                NotLoggedPnl.Visible = true;
                LoggedInPnl.Visible = false;
            }
            else
            {
                NotLoggedPnl.Visible = false;
                LoggedInPnl.Visible = true;
            }
        }
        protected void Unnamed_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("~/default.aspx");
        }

        protected void LogOutBtn_Click(object sender, EventArgs e)
        {
            HttpCookie myCookie = new HttpCookie("UserID");
            myCookie.Value = "-1";
            myCookie.Expires = DateTime.Now.AddDays(1d);
            Response.Cookies.Add(myCookie);
            Response.Redirect("~/default.aspx");
        }
    }
}