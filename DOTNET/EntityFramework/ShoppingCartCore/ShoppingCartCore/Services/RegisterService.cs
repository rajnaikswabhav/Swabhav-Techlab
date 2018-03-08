using ShoppingCartCore.Framework.Model;
using ShoppingCartCore.Framework.Repository.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartCore.Services
{
    public class RegisterService
    {

        public void RegisterUser(User user)
        {
            EntityFrameworkRepository<User> userRepo = new EntityFrameworkRepository<User>();
            userRepo.Add(user);
        }

        public string VerifyByEmail(string emailId, string userName)
        {
            var fromAddress = new MailAddress("akashmalaviya001@gmail.com", "BookmarkApp Team");
            var toAddress = new MailAddress(emailId, userName);
            string otp = GenerateOTP();
            const string subject = "Verify Email Address";
            string body = "Hello " + userName + " You recive this email because you register"
                + " with BookmarkApp. Your OTP is " + otp;

            var smtp = new SmtpClient
            {

                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("akashmalaviya001@gmail.com", "firetigo"),
                Timeout = 20000
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body

            })
            {
                smtp.Send(message);
            }
            return otp;
        }

        public string GenerateOTP()
        {
            string numbers = "1234567890";
            int length = 6;
            string otp = string.Empty;

            for (int i = 0; i < length; i++)
            {
                string number = string.Empty;
                do
                {
                    int index = new Random().Next(0, numbers.Length);
                    number = numbers.ToCharArray()[index].ToString();
                } while (otp.IndexOf(number) != -1);
                otp += number;
            }
            return otp;
        }
    }
}
