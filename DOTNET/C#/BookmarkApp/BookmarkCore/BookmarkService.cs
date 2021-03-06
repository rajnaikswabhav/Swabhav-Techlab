﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
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
            }
            reader.Close();
            connection.Close();
            return false;
        }

        public void RegisterUser(User user)
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("Insert Into Bookmark Values (@UNAME,@UPASS,@ULOC,@EMAIL)", connection);
            cmd.Parameters.AddWithValue("@UNAME", user.UserName);
            cmd.Parameters.AddWithValue("@UPASS", user.Password);
            cmd.Parameters.AddWithValue("@ULOC", user.Location);
            cmd.Parameters.AddWithValue("@EMAIL", user.Email);
            int i = cmd.ExecuteNonQuery();
            connection.Close();
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

        public void Save(string url, int id)
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("Insert Into UserBookmarks VALUES(@BNAME,@bookmarkId)", connection);
            cmd.Parameters.AddWithValue("@BNAME", url);
            cmd.Parameters.AddWithValue("@bookmarkId", id);
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public int GetUserByName(string userName)
        {
            int id = 0;
            connection.Open();
            SqlCommand cmd = new SqlCommand("Select ID From Bookmark where UNAME=@name", connection);
            cmd.Parameters.Add("@name", SqlDbType.VarChar);
            cmd.Parameters["@name"].Value = userName;
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                id = Convert.ToInt32(reader["ID"]); ;
            }
            connection.Close();
            if (id == 0)
            {
                return id;
            }
            else
            {
                return id;
            }
        }

        public DataSet GetBookmarks(int id)
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand("Select BNAME From UserBookmarks Where bookmarkId=" + id, connection);
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            adapter.SelectCommand = cmd;
            adapter.Fill(dataSet, "bookmarkTable");
            connection.Close();
            return dataSet;
        }

        public void ExportToHTMl(string userName, int id)
        {
            DataSet set = GetBookmarks(id);

            StreamWriter writer = new StreamWriter("C:/Users/Welcome/Desktop/" + userName + ".html", true);
            string parseToHTMl = "<html><head><title>Bookmarks</title></head><br><body><h2>Your Bookmarks @"
                + userName + "</h2><br><br>";

            foreach (DataTable table in set.Tables)
            {
                foreach (DataRow r in table.Rows)
                    parseToHTMl += "<li><a href=" + r.ToString() + "></a><br></li>";
            }

            parseToHTMl += "<br><h2>End</h2></body></html>";
            writer.Write(parseToHTMl);
            writer.Close();
        }
    }
}
