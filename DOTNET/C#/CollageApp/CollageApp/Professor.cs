using System;
using System.Collections.Generic;
using System.Text;

namespace CollageApp
{
    class Professor : Person, ISalaryCalculator
    {
        private double salary;
        public Professor(int id, string address, string dob) : base(id, address, dob)
        {
        }

        public double Salary { get { return salary; } }

        public double CalculateSalary(int days)
        {
            double oneDaySalary = 1000;
            salary = days * oneDaySalary;
            return salary;
        }

        public override string ToString()
        {
            String details = "Professor Details....\n";
            details += "Id: " + Id + "\n";
            details += "Address: " + Address + "\n";
            details += "DOB: " + Dob + "\n";
            details += "Salary: " + Salary + "\n";
            return details;
        }
    }
}
