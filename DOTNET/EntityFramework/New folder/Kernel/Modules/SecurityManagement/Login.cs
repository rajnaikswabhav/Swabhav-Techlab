using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;
using Techlabs.Euphoria.Kernel.Model;
using Techlabs.Euphoria.Kernel.Modules.SecurityManagement;

namespace Modules.SecurityManagement
{
    [Table("LOGIN")]
    public class Login : MasterEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; }
        public string EmailId { get; set; }
        public string Color { get; set; }

        public virtual Partner Partner { get; set; }
        public virtual Role Role { get; set; }
        public Login()
        {
        }

        public Login(string userName, string password, string roleName, string emailId, string color) : this()
        {
            Validate(userName, password, roleName);

            this.UserName = userName;
            this.Password = password;
            this.RoleName = roleName;
            this.EmailId = emailId;
            this.Color = color;
        }

        private static void Validate(string userName, string password, string roleName)
        {
            if (String.IsNullOrEmpty(userName))
                throw new ValidationException("Invalid RoleName");
        }

        public static Login Create(string userName, string password, string roleName, string emailId, string color)
        {
            return new Login(userName, password, roleName, emailId, color);
        }

        public void Update(string userName, string password, string roleName, string emailId,string color)
        {
            Validate(userName, password, roleName);

            this.UserName = userName;
            this.Password = password;
            this.RoleName = roleName;
            this.EmailId = emailId;
            this.Color = color;
        }
    }
}
