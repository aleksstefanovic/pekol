using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
            string address = null, postalCode = null, creditCard = null, cvv = null, expiryDate = null, email = null;

            try
            {
                address = (string)Application["Address"];
                postalCode = (string)Application["PostalCode"];
                creditCard = ((int)Application["CreditCard"]).ToString();
                cvv = ((int)Application["CVV"]).ToString();
                expiryDate = (string)Application["ExpDate"];
                email = (string)Application["Email"];

                if (address != null || postalCode != null || creditCard != null || cvv != null || expiryDate != null || email != null)
                {
                    string currentText = lblMessage.Text;
                    string newText = currentText + "<br />1 x " + item;
                    lblMessage.Text = newText;
                }
                else
                {
                    success = false;
                    lblMessage.Text = "Missing info from your profile! Please make sure you have a valid address, postal code, email and credit card setup on your account!";
                }
            }
            catch (Exception ex)
            {
                success = false;
                lblMessage.Text = "Missing info from your profile! Please make sure you have a valid address, postal code, email and credit card setup on your account!";
            }
            return success;
        }

        public static void CreateEmail(string email, string body, string subject, string name)
        {
            var fromAddress = new MailAddress("peckolpreventionproject@gmail.com", "The Peckol Prevention Project");
            var toAddress = new MailAddress(email, name);
            const string fromPassword = "stop2pekol";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                //this email css will probably have difficulty rendering, probably won't work 90% of the time. Email html is bad, would take like a week to perfect this thing
                Body =
                "<div> <h1 style=\"border-style: solid;#border-color: #8C41F4;border-color: #A742F4;border-radius: 15px 50px;margin:30px 30px 10px 10px;padding: 10px 10px 10px 10px;color:#FFFFFF; font-family: main;box-shadow: 0 0 0 10px hsl(0, 0%, 80%),0 0 0 15px hsl(0, 0%, 90%);background-color: #A742F4;font-size: 32px;\">The Peckol Prevention Project</h1> <p style=\"color:#000000; border-style: solid;#border-color: #8C41F4;border-color: #A742F4;border-radius: 15px 50px;margin:30px 30px 10px 10px;padding: 10px 10px 10px 10px;#color:#FFFFFF; font-family: main;box-shadow: 0 0 0 10px hsl(0, 0%, 80%),0 0 0 15px hsl(0, 0%, 90%);\"><p style=\"font-size:1.5vw; #font-size:24px; font-style:italic oblique;margin:2vw 2vw 1vw 1vw;\">We have received your order "
                + name
                + "!</p><p style=\"font-size:1vw; font-family:Calibri;margin:1vw 1vw 1vw 1vw;\">"
                + body
                + "</p> <p style=\"font-size:1.5vw; #font-size:24px; font-style:italic oblique;margin:2vw 2vw 1vw 1vw;\">Thank you for shopping at the Peckol Prevention Project!</p></p> <p style=\"border-style: solid;#border-color: #8C41F4;border-color: #A742F4;border-radius: 15px 50px;margin:30px 30px 10px 10px;padding: 10px 10px 10px 10px;color:#FFFFFF; font-family: main;box-shadow: 0 0 0 10px hsl(0, 0%, 80%),0 0 0 15px hsl(0, 0%, 90%);background-color: #A742F4;text-align: center;\">Copyright © The Peckol Prevention Project</p></div>",
                IsBodyHtml = true
            })
            {
                smtp.Send(message);
            }
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
            string content = lblMessage.Text;
            bool success = addToCart("");
            if (success)
            {
                string email = (string)Application["Email"];
                string userName = (string)Application["UserName"];
                CreateEmail(email, content, "Your Peckol Purchase Package!", userName);
                lblMessage.Text = "Successfully checked out! Please check your email to ensure you have received a confirmation email!";
            }
        }
    }
}