using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace BasicConnectionApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DevelopmentServer"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                Console.WriteLine("Connection State: " + connection.State);
                Console.WriteLine("Database is: " + connection.Database);
                Console.WriteLine("Conection Timeout: " + connection.ConnectionTimeout);
                Console.WriteLine("Client Id: " + connection.ClientConnectionId);
                Console.WriteLine("Connection container: " + connection.Container);
                Console.WriteLine("Credential: " + connection.Credential);
                Console.WriteLine("Data Source: " + connection.DataSource);
                Console.WriteLine("Site: " + connection.Site);
                Console.WriteLine("Server version: " + connection.ServerVersion);
                Console.WriteLine("WorkStation Id: " + connection.WorkstationId);
                Console.WriteLine("Packet Size: " + connection.PacketSize);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            finally
            {

                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                    Console.WriteLine("Connection State: " + connection.State);
                }
            }
            Console.ReadKey();
        }
    }
}
