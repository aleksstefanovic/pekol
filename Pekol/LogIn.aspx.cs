using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using HashLibrary;

namespace Pekol
{
    public partial class LogIn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool loggedIn = false;
            try
            {
                loggedIn = (bool) Application["LoggedIn"];
                if(loggedIn)
                {
                    logInDiv();
                }
                else
                {
                    logOutDiv();
                }
            }
            catch (Exception ex)
            {
                logOutDiv();
            }
        }

        public void logInDiv ()
        {
            Login1.Visible = false;
            CreateUser.Visible = false;
            userPage.Visible = true;
        }

        public void logOutDiv ()
        {
            Login1.Visible = true;
            CreateUser.Visible = true;
            userPage.Visible = false;
        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            bool authenticated = this.ValidateCredentials(Login1.UserName, Login1.Password);

            if (authenticated)
            {
                logInDiv();
                Response.Redirect(Request.RawUrl);
            }
        }

        public bool IsAlphaNumeric(string text)
        {
            return Regex.IsMatch(text, "^[a-zA-Z0-9]+$");
        }

        private bool ValidateCredentials(string userName, string password)
        {
            bool returnValue = false;

            if (userName.Length <= 50 && password.Length <= 50)
            {
                SqlConnection conn = null;

                try
                {
                    string sql = "select count(*) from users where username = @username and password = @password";

                    conn = new SqlConnection(ConfigurationManager.ConnectionStrings["pekolDB"].ConnectionString);
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    SqlParameter user = new SqlParameter();
                    user.ParameterName = "@username";
                    user.Value = userName.Trim();
                    cmd.Parameters.Add(user);

                    SqlParameter pass = new SqlParameter();
                    pass.ParameterName = "@password";
                    pass.Value = Hasher.HashString(password.Trim());
                    cmd.Parameters.Add(pass);

                    conn.Open();

                    int count = (int)cmd.ExecuteScalar();

                    if (count > 0)
                    {
                        Application["UserName"] = userName;
                        Application["Password"] = password;
                        Application["LoggedIn"] = true;
                        returnValue = true;
                    }
                }
                catch (Exception ex)
                {
                    // Log your error
                }
                finally
                {
                    if (conn != null) conn.Close();
                }
            }
            else
            {
                // Log error - user name not alpha-numeric or 
                // username or password exceed the length limit!
            }

            return returnValue;
        }

        private bool CreateUserAccount(string userName, string password)
        {
            bool created = false;
            SqlConnection conn = null;

            try
            {
                string sql = "INSERT INTO Users VALUES (@username, @password);";

                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["pekolDB"].ConnectionString);
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlParameter user = new SqlParameter();
                user.ParameterName = "@username";
                user.Value = userName.Trim();
                cmd.Parameters.Add(user);

                SqlParameter pass = new SqlParameter();
                pass.ParameterName = "@password";
                pass.Value = Hasher.HashString(password.Trim());
                cmd.Parameters.Add(pass);

                conn.Open();

                object count = cmd.ExecuteScalar();
                created = true;
            }
            catch (Exception ex)
            {
                // Log your error
            }
            finally
            {
                if (conn != null) conn.Close();
            }
            return created;
        }

        protected void CreateUser_Click(object sender, EventArgs e)
        {
            bool created = this.CreateUserAccount(Login1.UserName, Login1.Password);
            if (created)
            {
                bool authenticated = this.ValidateCredentials(Login1.UserName, Login1.Password);

                if (authenticated)
                {
                    logInDiv();
                }
            }
        }
    }
}