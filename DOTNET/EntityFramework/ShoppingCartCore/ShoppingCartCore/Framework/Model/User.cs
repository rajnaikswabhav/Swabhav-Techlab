using ShoppingCartCore.Framework.Enums;
using ShoppingCartCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartCore.Framework.Model
{
    
    public class User : Entity
    {

        public User() { }

        public User(string firstName,string lastName,string phoneNo,int age,string gender
            ,string role,string email,string pass,string profilePic) {

            FirstName = firstName;
            LastName = lastName;
            PhoneNo = phoneNo;
            Age = age;
            Gender = gender;
            Role = role;
            Email = email;
            Password = pass;
            ProfilePhoto = profilePic;
        }

        public string FirstName  { get; set; }
        public string LastName  { get; set; }
        public string PhoneNo  { get; set; }
        public int Age { get; set; }
        public string Gender  { get; set; }
        public string Role  { get; set; }
        public string Email  { get; set; }
        public string Password  { get; set; }
        public string ProfilePhoto { get; set; }
    }
}
