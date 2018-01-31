using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeLibrary
{
    public abstract class Employee
    {

        public String EmployeeName
        {
            get;
            set;
        }

        public DateTime DateOfBirth
        {
            get;
            set;
        }

        public double BasicSalary
        {
            get;
            set;
        }

        public double HouseRentAllowance
        {
            get
            {
               return  (BasicSalary * 50) / 100;
                
            }
            
        }

        public double DernessAllowance
        {
            get
            {
                return (BasicSalary * 40) / 100;
            }
        }

        public double Performance
        {
            get
            {
                return (BasicSalary * 20) / 100;
            }
        }

        public float CalculateAge()
        {
            float dob = 0;
            DateTime birthDate = DateOfBirth;
            DateTime todayDate = System.DateTime.Today;
            var todayYear = todayDate.Year;
            var birthYear = birthDate.Year;
            dob = todayYear-birthYear;

            var todayMonth = todayDate.Month;
            var birthMonth = birthDate.Month;

            if (birthMonth > todayMonth) {
                dob--;
            }
            else if(birthMonth == todayMonth)
            {
                var currentDay = todayDate.Day;
                var birthDay = birthDate.Day;

                if(birthDay > currentDay)
                {
                    dob--;
                }
            }
            return dob;
        }

        public abstract double CalculateSalary();
    }
}
