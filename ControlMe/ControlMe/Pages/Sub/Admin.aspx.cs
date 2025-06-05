using SqlHelper;
using CryptoHelp;
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
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string ID = "";
                SqlHelp shelp = new SqlHelp();
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
                    { }
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
                if (ID == "1")
                {
                    Main.Visible = true;
                }


                shelp = new SqlHelp();
                ConnectionStr = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Environ"].ToString()].ConnectionString; ;
                errorstr = "";
                commandstr = "";
                commandstr = "exec [dbo].[USP_AdminCommandLog]";
                DataSet ds = shelp.DatasetRet(commandstr, ConnectionStr, out errorstr);
                if (errorstr != "")
                { Errorstrlbl.Text = errorstr; }
                else
                {
                    CryptoHelper cryhlp = new CryptoHelper();
                    string todecry = "";
                    string decry = "";
                    DataTable dt = ds.Tables[0];

                    foreach (DataRow dr in dt.Rows)
                    {
                        todecry = dr["Content"].ToString();
                        decry = "";
                        foreach (string str in todecry.Split(new[] { "|||" }, System.StringSplitOptions.RemoveEmptyEntries))
                        {
                            try
                            {
                                decry += cryhlp.Decrypt(str) + "|||";
                            }
                            catch (Exception ex)
                            {
                                decry += "Error|||";

                            }
                        }
                        dr["Decrypt"] = decry;
                    }

                    DataGrid1.DataSource = dt; // dataset
                    DataGrid1.DataBind();
                }
                commandstr = "exec [dbo].[Usp_GetVerificationReq]";
                ds = shelp.DatasetRet(commandstr, ConnectionStr, out errorstr);
                if (errorstr != "")
                { Errorstrlbl.Text = errorstr; }
                else
                {
                    DataTable dt = ds.Tables[0];
                    DataGrid2.DataSource = dt; // dataset
                    DataGrid2.DataBind();
                }
                commandstr = "exec [dbo].[usp_bestsender] ";
                try
                {
                    ds = shelp.DatasetRet(commandstr, ConnectionStr, out errorstr);
                    if (errorstr != "")
                    { Errorstrlbl.Text = errorstr; }
                    else
                    {
                        DataTable dt = ds.Tables[0];
                        Scores.DataSource = dt; // dataset
                        Scores.DataBind();
                    }
                }
                catch (Exception ex)
                {
                    Errorstrlbl.Text = ex.Message;
                }
            }
            catch (Exception ex)
            {
                Errorstrlbl.Text = ex.Message;
            }

        }

        protected void Unnamed1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlHelp shelp = new SqlHelp();
                string ConnectionStr = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Environ"].ToString()].ConnectionString; ;
                string errorstr = "";
                string commandstr = "";
                commandstr = "truncate table dbo.CmdLog";
                shelp.NoReturn(commandstr, ConnectionStr, out errorstr);
                if (errorstr != "")
                { Errorstrlbl.Text = errorstr; }
            }
            catch (Exception ex)
            {
                Errorstrlbl.Text = ex.Message;
            }
        }
        protected void Verified_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Done")
            {
                string usernm = e.Item.Cells[0].Text;
                SqlHelper.SqlHelp shelp = new SqlHelp();
                string ConnectionStr = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Environ"].ToString()].ConnectionString;
                string errorstr;
                string commandstr = "";
                commandstr = "exec [dbo].[usp_veriySent] '" + usernm + "'";
                string rows = shelp.RowsAffect(commandstr, ConnectionStr, out errorstr).ToString();
                if (errorstr != "")
                { Errorstrlbl.Text = errorstr; }
            }
        }
        protected void DataGrid1_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            { // Get the selected item's data
              //
              // Perform your logic here
                string command = e.Item.Cells[5].Text;

                string usernm = "Krystal";
                string comm = command;
                string all = "0";
                string fromuser = usernm;
                string frompword = "_9Rubber9_";

                SqlHelper.SqlHelp shelp = new SqlHelp();
                string ConnectionStr = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Environ"].ToString()].ConnectionString;
                string errorstr;
                string commandstr = "";
                commandstr = "exec usp_SendAppCmd '" + usernm + "','" + comm + "', " + all + ",'" + fromuser + "', '" + frompword + "'";
                string rows = shelp.RowsAffect(commandstr, ConnectionStr, out errorstr).ToString();
                if (errorstr != "")
                { Errorstrlbl.Text = errorstr; }
            }
        }

        protected void getdata_Click(object sender, EventArgs e)
        {
            SqlHelp shelp = new SqlHelp();
            string errorstr;
            string ConnectionStr = ConfigurationManager.ConnectionStrings[ConfigurationManager.AppSettings["Environ"].ToString()].ConnectionString; ;
            string commandstr = "exec [dbo].[Usp_userinfo] '" + screenname.Text + "'," + userid.Text;
            DataSet ds = shelp.DatasetRet(commandstr, ConnectionStr, out errorstr);
            if (errorstr != "")
            { Errorstrlbl.Text = errorstr; }
            else
            {
                DataTable dt = ds.Tables[0];
                DataGrid3.DataSource = dt; // dataset
                DataGrid3.DataBind();
            }
        }
    }
}