using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeApp2
{
    class Employee
    {
        private readonly String id;
        private readonly String name;
        private readonly String designation;
        private readonly String salary;
        private readonly String joiningDate;
        private readonly String dearnessAllownce;
        private readonly String departments;

        public Employee(String id, String name, String designation, String salary,
            String joiningDate, String dearnessAllownce, String departments)
        {
            this.id = id;
            this.name = name;
            this.salary = salary;
            this.designation = designation;
            this.joiningDate = joiningDate;
            this.dearnessAllownce = dearnessAllownce;
            this.departments = departments;
        }

        public String Id
        {
            get
            {
                return id;
            }
        }

        public String Name { get { return name; } }
        public String Designation { get { return designation; } }
        public String Salary { get { return salary; } }
        public String JoiningDate { get { return joiningDate; } }
        public String DearnessAllownce { get { return dearnessAllownce; } }
        public String Department { get { return departments; } }

        public override string ToString()
        {
            return Id + "," + Name + "," + Designation + "," + Salary + ","
                + JoiningDate + "," + DearnessAllownce + "," + Department;
        }
    }
}
