using System;
using System.Collections.Generic;
using System.IO;

namespace EmployeeApp2
{
    class EmployeeService
    {
        Employee employee;
        HashSet<String> employeeDetails = new HashSet<string>();
        HashSet<Employee> employees = new HashSet<Employee>();

        public void IntializeList()
        {
            String detail;
            try
            {
                StreamReader reader = new StreamReader("dataFile.txt");
                detail = reader.ReadLine();
                
                while(detail != null)
                {
                    employeeDetails.Add(detail);
                    detail = reader.ReadLine();
                }

                reader.Close();
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public HashSet<Employee> get()
        {
            foreach (String detail in employeeDetails)
            {
                char[] commaSeparators = new char[] { ',' };
                String[] details = detail.Split(commaSeparators);
                details[1] = details[1].Replace("'", "");
                details[2] = details[2].Replace("'","");
                details[4] = details[4].Replace("'","");
                details[6] = details[6].Replace(details[6],details[7]);
                employee = new Employee(details[0],details[1],details[2],details[3],
                    details[4],details[5],details[6]);
                employees.Add(employee);
            }

            return employees;
        }
    }
}
