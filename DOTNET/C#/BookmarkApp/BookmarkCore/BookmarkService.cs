using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookmarkCore
{
    class BookmarkService
    {
        private static string connectionString = ConfigurationManager.ConnectionStrings["DevelopmentServer"].ConnectionString;
        SqlConnection connection = new SqlConnection(connectionString);

        public bool CheckLogin(string userName , string password)
        {
            SqlCommand cmd = new SqlCommand("Select ENAME,EPASS from Bookmark",connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if(userName.Equals(reader["ENAME"]) && password.Equals(reader["EPASS"])) {
                    return true;
                }
                else
                {

                    return false;
                }
            }                  
            return false;
        }

        public void RegisterUser(User user)
        {

        }
    }
}
