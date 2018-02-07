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
            Console.WriteLine("Employee id: ");
            string empId = Console.ReadLine();
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand("SELECT * FROM EMP WHERE EMPNO ="
              +empId,connection);
                      
            try
            {
                connection.Open();
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    Console.WriteLine(dataReader["EMPNO"]+","+dataReader["ENAME"]+ ","+dataReader["JOB"]+ ","+
                        dataReader["MGR"]+ ","+ dataReader["HIREDATE"]+ ","+ dataReader["SAL"]+ ","+ dataReader["COMM"]+
                        ","+dataReader["DEPTNO"] );
                }

                dataReader.Close();
                connection.Close();
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception);
            }
        }
    }
}
