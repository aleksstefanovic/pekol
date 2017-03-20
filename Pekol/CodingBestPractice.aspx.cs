using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pekol
{
    public partial class CodingBestPractice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void submitBtn_Click(object sender, EventArgs e)
        {
            //CreateEmail();
        }

        public static void CreateEmail()
        {
            var fromAddress = new MailAddress("pekolpreventionproject@gmail.com", "The Peckol Prevention Project");
            var toAddress = new MailAddress("aleks.stefanovic101@gmail.com", "?");
            const string fromPassword = "";
            const string subject = "Subject";
            const string body = "Body";

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
                Body = body
            })
            {
                smtp.Send(message);
            }
        }

        protected void codeUpload_Load(object sender, EventArgs e)
        {
        }

        protected void codeUpload_DataBinding(object sender, EventArgs e)
        {
        }
    }
}