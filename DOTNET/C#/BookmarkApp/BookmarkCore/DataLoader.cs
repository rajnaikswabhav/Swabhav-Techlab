using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookmarkCore
{
    class DataLoader
    {
        private List<User> registerUser = new List<User>();
        public void LoadData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DevelopmentServer"].ConnectionString;
            SqlConnection connection = new SqlConnection();
            SqlCommand cmd = new SqlCommand("Select * from Bookmark",connection);
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataSet dataSet = new DataSet();
            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    User user = new User(reader["ENAME"],reader["EPASS"],reader["EMAIL"],reader["LOC"]);
                }
                connection.Close();


            } 
            catch(Exception e)
            {
                Console.WriteLine("Exception" + e);
            }
        }
    }
}
