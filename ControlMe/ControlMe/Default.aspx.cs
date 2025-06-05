using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ControlMe
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie myCookie = new HttpCookie("UserID");
            string userid = myCookie.Value;
            if (userid != null)
            {
                greet.Text = "You are all logged in an ready to go.";
            }
        }
    }
}