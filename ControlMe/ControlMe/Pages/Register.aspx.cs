using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SqlHelper;
using System.Configuration;

namespace ControlMe.Pages
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SubmitReg_Click(object sender, EventArgs e)
        {
            
            SqlHelper.SqlHelp shelp = new SqlHelp();
            string ConnectionStr = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Environ"].ToString()].ConnectionString;;
            string errorstr;
            string commandstr = "";
            commandstr = "exec usp_checkexisting '" + ScreenNameTxt.Text + "','" + EmailTxt.Text + "'";
            int rowsint = shelp.RowCOunt(commandstr, ConnectionStr, out errorstr);
            if (rowsint > 0)
            {
                errorlbl.Text = "User or email already exists";
            }
            else
            {
                if (errorstr.Length > 0)
                {
                    errorlbl.Text = errorstr;
                }
                else
                {
                    int opt = 0;
                    if(RndOpt.Text== "Opt In")
                        opt= 1;

                    commandstr = "exec usp_register '" + ScreenNameTxt.Text + "','" + EmailTxt.Text + "','" + PasswordTxt.Text + "','" + DDRoleTxt.Text + "',"+ opt.ToString();
                    shelp.NoReturn(commandstr, ConnectionStr, out errorstr);
                    if (errorstr.Length > 0)
                    {
                        errorlbl.Text = errorstr;
                    }
                    else
                    {
                        Response.Redirect("../default.aspx");
                    }
                }
            }
        }
    }
}