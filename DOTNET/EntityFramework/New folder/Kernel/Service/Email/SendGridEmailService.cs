using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using System.Net;
using System.Net.Mail;
using SendGrid;
using System.Configuration;

namespace Techlabs.Euphoria.Kernel.Service.Email
{
    public class SendGridEmailService : IEmailService
    {
        private string _port,
                       _host,
                       _username,
                       _password,
                       _sender,
                       _senderPassword;

        public SendGridEmailService() {

            _port = ConfigurationManager.AppSettings["sendgrid.port"];
            _host = ConfigurationManager.AppSettings["sendgrid.host"];
            _username = ConfigurationManager.AppSettings["sendgrid.username"];
            _password = ConfigurationManager.AppSettings["sendgrid.password"];
            _sender = ConfigurationManager.AppSettings["sendgrid.sender"];
            _senderPassword = ConfigurationManager.AppSettings["sendgrid.sender.pwd"];

        }



        private async Task SendMailAsync(string visitorEmailId, string subject, string body, string attachementFile = null)
        {

            var client = new SmtpClient
            {
                Port = int.Parse(_port),
                Host = _host,
                Timeout = 10000,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false
            };

            client.Credentials = new NetworkCredential(_username, _password);

            var mail = new MailMessage
            {
                From = new MailAddress(_sender),
                Subject = subject,
                Body = body
            };
            mail.IsBodyHtml = true;

            const string state = "token";
            client.SendCompleted += Client_SendCompleted; ;

            if (visitorEmailId != null)
            {
                // Remember that MIME To's are different than SMTPAPI Header To's!
                mail.To.Add(new MailAddress(visitorEmailId));
                  client.SendAsync(mail, state);
             // await client.SendMailAsync(mail);
               
               
            }



        //    mail.Dispose();



        }

        private void Client_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            var token = (string)e.UserState;

            if (e.Cancelled)
            {
                Console.WriteLine("[{0}] Send canceled.", token);
            }
            if (e.Error != null)
            {
                Console.WriteLine("[{0}] {1}", token, e.Error);
            }
            else
            {
                Console.WriteLine("Message sent.");
            }
        }

       public void Send(string visitorEmailId, string subject, string body, string attachementFile = null)
        {
            SendMailAsync(visitorEmailId, subject, body, attachementFile);
        }
    }
}
