using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ParameterizedQueryApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Employee Id: ");
            string empId = Console.ReadLine();
            string connectionString = ConfigurationManager.ConnectionStrings["DevelopmentServer"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("SELECT * FROM EMP WHERE EMPNO = @empNo",connection);
            command.Parameters.Add("@empNo",SqlDbType.Int);
            command.Parameters["@empNo"].Value = empId;

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine(reader["EMPNO"] + "," + reader["ENAME"] + "," + reader["JOB"] + "," +
                        reader["MGR"] + "," + reader["HIREDATE"] + "," + reader["SAL"] + "," + reader["COMM"] +
                        "," + reader["DEPTNO"]);
                }
                reader.Close();
                connection.Close();
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}
