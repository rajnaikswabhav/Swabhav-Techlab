using System;
using System.Configuration;
using System.Data.SqlClient;

namespace basicCommandApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DevelopmentServer"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionString);
        }
    }
}
