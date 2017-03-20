using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pekol
{
    public partial class Shop : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool loggedIn = false;
            try
            {
                loggedIn = (bool) Application["LoggedIn"];
                if(!loggedIn)
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Default.aspx");
            }
        }

        private bool addToCart (string item)
        {
            bool success = true;
            string address = null, postalCode = null, creditCard = null, cvv = null, expiryDate = null;

            try
            {
                address = (string)Application["Address"];
                postalCode = (string)Application["PostalCode"];
                creditCard = ((int)Application["CreditCard"]).ToString();
                cvv = ((int)Application["CVV"]).ToString();
                expiryDate = (string)Application["ExpDate"];

                if (address != null || postalCode != null || creditCard != null || cvv != null || expiryDate != null)
                {
                    string currentText = lblMessage.Text;
                    string newText = currentText + "<br />1 x " + item;
                    lblMessage.Text = newText;
                }
                else
                {
                    success = false;
                    lblMessage.Text = "Missing info from your profile! Please make sure you have a valid address, postal code, and credit card setup on your account!";
                }
            }
            catch (Exception ex)
            {
                success = false;
                lblMessage.Text = "Missing info from your profile! Please make sure you have a valid address, postal code, and credit card setup on your account!";
            }
            return success;
        }

        protected void addGloves_Click(object sender, EventArgs e)
        {
            addToCart("Defensive Gloves");
        }

        protected void addBook_Click(object sender, EventArgs e)
        {
            addToCart("Peckol Prevention Handbook");
        }

        protected void addDisguise_Click(object sender, EventArgs e)
        {
            addToCart("Anti Peckol Disguise");
        }

        protected void addRepel_Click(object sender, EventArgs e)
        {
            addToCart("Peckol Repellent");
        }

        protected void checkout_Click(object sender, EventArgs e)
        {
            bool success = addToCart("");
            if (success)
            {
                lblMessage.Text = "Successfully checked out! Please check your email to ensure you have received a confirmation email!";
            }
        }
    }
}