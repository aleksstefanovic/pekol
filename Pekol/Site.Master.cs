using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pekol
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool loggedIn = false;
            try
            {
                loggedIn = (bool) Application["LoggedIn"];
                if(loggedIn)
                {
                    userLabel.Visible = true;
                    LogOut.Visible = true;
                    userLabel.InnerText = "Welcome " + Application["UserName"] + "!";
                }
                else
                {
                    userLabel.Visible = false;
                    LogOut.Visible = false;
                }
            }
            catch (Exception ex)
            {
                userLabel.Visible = false;
                LogOut.Visible = false;
            }

        }

        protected void LogOut_Click(object sender, EventArgs e)
        {
            Application["UserName"] = "";
            Application["Password"] = "";
            Application["LoggedIn"] = false;
            Response.Redirect(Request.RawUrl);
        }
    }
}