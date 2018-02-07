using System;
using System.Configuration;
using System.Data.SqlClient;

namespace BasicTransactionApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DevelopmentServer"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd1 = new SqlCommand("UPDATE Merchant SET balance = balance + 250",connection);
            SqlCommand cmd2 = new SqlCommand("UPDATE Customer SET balance = balance - 250",connection);

            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();
            cmd1.Transaction = transaction;
            cmd2.Transaction = transaction;

            try
            {
                cmd1.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
                transaction.Commit();
                Console.WriteLine("Transaction Successfull...");
            }
            catch (Exception)
            {
                transaction.Rollback();
                Console.WriteLine("Transaction Failed.....");
            }
            connection.Close();

        }
    }
}
