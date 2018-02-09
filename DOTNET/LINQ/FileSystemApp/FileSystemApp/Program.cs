using EmployeeApp2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //LinqWithFileSystem();
            String line;
            char[] commaSepretaor = new char[] { ',' };
            HashSet<String> employees = new HashSet<String>();
            try
            {
                StreamReader reader = new StreamReader("dataFile.txt");
                line = reader.ReadLine();
                while (line != null)
                {
                    employees.Add(line);
                    line = reader.ReadLine();
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            HashSet<Employee> listOfEmployee = new HashSet<Employee>();
            foreach (var detail in employees)
            {
                string[] details = detail.Split(commaSepretaor);
                Employee employee = new Employee(details[0], details[1], details[2], details[3], details[4],
                    details[5], details[6]);
                listOfEmployee.Add(employee);
            }

            var displayEmployees = listOfEmployee.OrderBy(e => e.Name);
            foreach (var employee in displayEmployees)
            {
                Console.WriteLine(employee);
            }

        }

        private static void LinqWithFileSystem()
        {
            DirectoryInfo info = new DirectoryInfo("C:/Windows/System32");
            FileInfo[] files = info.GetFiles();
            var top3FilesByitsSize = files.OrderBy(f => f.Length).Take(3);
            foreach (var file in top3FilesByitsSize)
            {
                Console.WriteLine("Filname: {0}, FileSize: {1}", file.Name, file.Length);
            }
        }
    }
}
