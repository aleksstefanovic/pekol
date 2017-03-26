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

                if (!IsPostBack)
                {
                    syncUserCreds();
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

        public void syncUserCreds ()
        {
            userName.Text = (string) Application["UserName"];
            password.Text = (string) Application["Password"];

            try
            {
                address.Text = (string)Application["Address"];
            }
            catch(Exception ex) { }

            try
            {
                postalCode.Text = (string)Application["PostalCode"];
            }
            catch(Exception ex) { }

            try
            {
                creditCard.Text = ((int) Application["CreditCard"]).ToString();
            }
            catch(Exception ex) { }

            try
            {
                cvv.Text = ((int) Application["CVV"]).ToString();
            }
            catch(Exception ex) { }

            try
            {
                expiryDate.Text = (string) Application["ExpDate"];
            }
            catch(Exception ex) { }

            try
            {
                phoneNumber.Text = (string) Application["PhoneNumber"];
            }
            catch(Exception ex) { }

            try
            {
                email.Text = (string) Application["Email"];
            }
            catch(Exception ex) { }
        }

        public void logOutDiv ()
        {
            Login1.Visible = true;
            CreateUser.Visible = true;
            userPage.Visible = false;
        }

        private bool isDuplicateUser (string userName)
        {
            bool isDuplicate = false;

            SqlConnection conn = null;

            try
            {
                string sql = "select count(*) from users where username = @username";

                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["pekolDB"].ConnectionString);
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlParameter user = new SqlParameter();
                user.ParameterName = "@username";
                user.Value = userName.Trim();
                cmd.Parameters.Add(user);

                conn.Open();

                int count = (int)cmd.ExecuteScalar();

                if (count > 0)
                {
                    isDuplicate = true;
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
            return isDuplicate;
        }

        private void setCurrentUserCreds (string UserName, string Password)
        {
                SqlConnection conn = null;

                try
                {
                    string sql = "SELECT * FROM Users WHERE UserName=@username";

                    conn = new SqlConnection(ConfigurationManager.ConnectionStrings["pekolDB"].ConnectionString);
                    SqlCommand cmd = new SqlCommand(sql, conn);

                    SqlParameter user = new SqlParameter();
                    user.ParameterName = "@username";
                    user.Value = UserName.Trim();
                    cmd.Parameters.Add(user);

                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Application["UserName"] = reader[0];
                        Application["Password"] = Password;
                        Application["LoggedIn"] = true;
                        Application["Address"] = reader[2];
                        Application["PostalCode"] = reader[3];
                        Application["CreditCard"] = reader[4];
                        Application["CVV"] = reader[5];
                        Application["ExpDate"] = reader[6];
                        Application["PhoneNumber"] = reader[7];
                        Application["Email"] = reader[8];
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

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            lblError.Text = "";
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
                        setCurrentUserCreds(userName, password);
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

            bool isDuplicate = isDuplicateUser(userName);
            if (!isDuplicate)
            { 

                try
                {
                    string sql = "INSERT INTO Users (UserName, Password) VALUES (@username, @password);";

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
            }
            else 
            {
                lblError.Text = "Duplicate User!";
            }
            return created;
        }

        protected void CreateUser_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            bool created = this.CreateUserAccount(Login1.UserName, Login1.Password);
            if (created)
            {
                AuthenticateEventArgs sample = new AuthenticateEventArgs();
                this.Login1_Authenticate(sender, sample);
            }
        }

        private void updateUserProfile(string oldUserName, string username, string Password, string Address, string PostalCode, string CreditCard, string CVV, string ExpDate, string PhoneNumber, string Email)
        {
            SqlConnection conn = null;
            try
            {
                string sql = "UPDATE Users SET UserName=@username, Password=@password, Address=@address, PostalCode=@postalCode, CreditCard=@creditCard, CVV=@cvv, ExpiryDate=@expDate, PhoneNumber=@phoneNumber, Email=@email WHERE UserName=@oldUserName;";


                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["pekolDB"].ConnectionString);

                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlParameter oldUser = new SqlParameter();
                oldUser.ParameterName = "@oldUserName";
                oldUser.Value = oldUserName.Trim();
                cmd.Parameters.Add(oldUser);

                SqlParameter user = new SqlParameter();
                user.ParameterName = "@username";
                user.Value = username.Trim();
                cmd.Parameters.Add(user);

                SqlParameter pass = new SqlParameter();
                pass.ParameterName = "@password";
                pass.Value = Hasher.HashString(Password.Trim());
                cmd.Parameters.Add(pass);

                SqlParameter add = new SqlParameter();
                add.ParameterName = "@address";
                add.Value = Address;
                cmd.Parameters.Add(add);

                SqlParameter psCode = new SqlParameter();
                psCode.ParameterName = "@postalCode";
                psCode.Value = PostalCode;
                cmd.Parameters.Add(psCode);

                SqlParameter cCard = new SqlParameter();
                cCard.ParameterName = "@creditCard";
                cCard.Value = Convert.ToInt32(CreditCard);
                cmd.Parameters.Add(cCard);

                SqlParameter cvv = new SqlParameter();
                cvv.ParameterName = "@cvv";
                cvv.Value = Convert.ToInt32(CVV);
                cmd.Parameters.Add(cvv);

                SqlParameter expDate = new SqlParameter();
                expDate.ParameterName = "@expDate";
                expDate.Value = ExpDate;
                cmd.Parameters.Add(expDate);

                SqlParameter phoneNumber = new SqlParameter();
                phoneNumber.ParameterName = "@phoneNumber";
                phoneNumber.Value = PhoneNumber;
                cmd.Parameters.Add(phoneNumber);

                SqlParameter email = new SqlParameter();
                email.ParameterName = "@email";
                email.Value = Email;
                cmd.Parameters.Add(email);

                conn.Open();

                object count = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                lblError.Text = "Invalid data, could not update profile!";
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }

        protected void saveUser_Click(object sender, EventArgs e)
        {
            string oldUserName = (string) Application["UserName"];
            string username = userName.Text;
            string Password = password.Text;
            string Address = address.Text;
            string PostalCode = postalCode.Text;
            string CreditCard = creditCard.Text;
            string CVV = cvv.Text;
            string ExpDate = expiryDate.Text;
            string PhoneNumber = phoneNumber.Text;
            string Email = email.Text;
            updateUserProfile(oldUserName, username, Password, Address, PostalCode, CreditCard, CVV, ExpDate, PhoneNumber, Email);
            setCurrentUserCreds(username, Password);
        }
    }
}