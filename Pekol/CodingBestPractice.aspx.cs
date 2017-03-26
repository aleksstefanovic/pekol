using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
            bool loggedIn = false;
            try
            {
                loggedIn = (bool) Application["LoggedIn"];
                if(loggedIn)
                {
                    addTip.Visible = true;
                }
                else
                {
                    addTip.Visible = false;
                }
            }
            catch (Exception ex)
            {
                addTip.Visible = false;
            }
        }

        protected void submitBtn_Click(object sender, EventArgs e)
        {
            //CreateEmail();
        }

        private bool isDuplicateTip (string tip)
        {
            bool isDuplicate = false;

            SqlConnection conn = null;

            try
            {
                string sql = "select count(*) from Tips where tip = @tipParam";

                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["pekolDB"].ConnectionString);
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlParameter tipParam = new SqlParameter();
                tipParam.ParameterName = "@tipParam";
                tipParam.Value = tip;
                cmd.Parameters.Add(tipParam);

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

        private bool insertNewTip(string tip)
        {
            bool created = false;
            SqlConnection conn = null;


            try
            {
                string sql = "INSERT INTO Tips (Tip) VALUES (@tip);";

                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["pekolDB"].ConnectionString);
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlParameter tipParam= new SqlParameter();
                tipParam.ParameterName = "@tip";
                tipParam.Value = tip;
                cmd.Parameters.Add(tipParam);

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

        protected void saveTip_Click(object sender, EventArgs e)
        {
            string newTip = tip.Text;
            bool duplicate = isDuplicateTip(newTip);
            if (!duplicate)
            {
                bool created = insertNewTip(newTip);
                if (created)
                {
                    UpdatePanelTable.Update();
                }
            }
        }
    }
}