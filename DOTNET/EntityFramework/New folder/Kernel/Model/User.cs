using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;

namespace Techlabs.Euphoria.Kernel.Model
{
    [Table("USER")]
    public class User : MasterEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public UserRole Role { get; set; }

        public User()
        {
        }

        private static void Validate(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName))
                throw new ValidationException("Invalid UserName");
            if (String.IsNullOrEmpty(userName))
                throw new ValidationException("Invalid Password");
        }

        public User(string userName, string password)
        {
            Validate(userName, password);
            this.UserName = userName;
            this.Password = password;
        }

        public static User Create(string userName, string password)
        {
            return new User(userName, password);
        }

        public void Update(string userName, string password)
        {
            Validate(userName, password);

            this.UserName = userName;
            Password = password;
        }
    }
}
