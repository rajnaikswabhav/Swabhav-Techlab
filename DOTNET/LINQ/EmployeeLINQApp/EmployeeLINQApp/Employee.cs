using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeLINQApp
{
    public class Employee
    {
        private int employeeId;
        private String employeeName;
        private String designation;
        private int? mGR;
        private String hireDate;
        private double salary;
        private double? commision;
        private int departmentNo;

        public Employee(int employeeId,String employeeName,String designation,int? mGR,String hireDate,
            double salary,double? commision,int departmentNo) {
            this.employeeId = employeeId;
            this.employeeName = employeeName;
            this.designation = designation;
            this.mGR = mGR;
            this.hireDate = hireDate;
            this.salary = salary;
            this.commision = commision;
            this.departmentNo = departmentNo;
        }
        public int EmployeeId { get { return employeeId; } }
        public String EmployeeName { get { return employeeName; } }
        public String Designation { get { return designation; } }
        public int? MGR { get { return mGR; } }
        public String HireDate { get { return hireDate; } }
        public double Salary { get { return salary; } }
        public double? Commision { get { return commision; } }
        public int DepartmentNo { get { return departmentNo; } }
    }
}
