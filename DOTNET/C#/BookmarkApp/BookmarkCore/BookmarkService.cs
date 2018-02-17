using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BookmarkCore
{
    public class BookmarkService
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["DevelopmentServer"].ConnectionString;
        SqlConnection connection = new SqlConnection(connectionString);

        public bool CheckLogin(string userName, string password)
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("Select UNAME,UPASS from Bookmark", connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (userName.Equals(reader["UNAME"]) && password.Equals(reader["UPASS"]))
                {
                    return true;
                }
                else
                {

                    return false;
                }
            }
            reader.Close();
            connection.Close();
            return false;
        }

        public void RegisterUser(User user)
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("Insert Into Bookmark(UNAME,UPASS,ULOC,EMAIL) Values (@UNAME,@UPASS,@ULOC,@EMAIL)", connection);
            cmd.Parameters.AddWithValue("@UNAME", user.UserName);
            cmd.Parameters.AddWithValue("@UPASS", user.Password);
            cmd.Parameters.AddWithValue("@ULOC", user.Location);
            cmd.Parameters.AddWithValue("@EMAIL", user.Email);

            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public string VerifyByEmail(string emailId, string userName)
        {
            var fromAddress = new MailAddress("akashmalaviya001@gmail.com", "BookmarkApp Team");
            var toAddress = new MailAddress(emailId, userName);
            const string fromPassword = "firetigo";
            string otp = GenerateOTP();
            const string subject = "Verify Email Address";
            string body = "Hello "+userName+" You recive this email because you register"
                +"with BookmarkApp. Your OTP is " + otp;

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

            }) {
                smtp.Send(message);
                return "Mail has been successfully sent.";
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
