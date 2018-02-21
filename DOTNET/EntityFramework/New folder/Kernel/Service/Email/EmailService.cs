using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Techlabs.Euphoria.Kernel.Service.Email
{
    public class EmailService
    {
        private async Task SendMailAsync(string visitorEmailId, string subject, string body,string attachementFile=null)
        {
            MailMessage mail = new MailMessage();

            mail.To.Add(visitorEmailId);

            mail.From = new MailAddress("info@gsmktg.com");

            mail.Subject = subject;

            mail.Body = body;

            mail.IsBodyHtml = true;
            var host= ConfigurationManager.AppSettings["EmailServer"];
            var port = Convert.ToInt32( ConfigurationManager.AppSettings["EmailPort"]);
            var fromEmail= ConfigurationManager.AppSettings["FromEmail"];
            var password= ConfigurationManager.AppSettings["LivePwd"];
            //var host = "smtp.gmail.com";
            //var port = 587;
            //var fromEmail= "info@swabhavtechlabs.com";
            //var password = "admin123";

            if (attachementFile != null)
            {
                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(attachementFile);
                mail.Attachments.Add(attachment);
            }

            SmtpClient smtp = new SmtpClient();
            smtp.Host = host;
            smtp.Port = port;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential
            (fromEmail, password);// Enter seders User name and password
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }
        public void Send(string visitorEmailId, string subject, string body, string attachementFile = null)
        {
            SendMailAsync(visitorEmailId, subject,body,attachementFile);
        }
    }
}
