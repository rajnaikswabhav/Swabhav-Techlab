using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewBagAndDataApp.Models
{
    public class Employee
    {
        private string name;
        private int age;
        private string email;
        private string city;

        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
    }
}