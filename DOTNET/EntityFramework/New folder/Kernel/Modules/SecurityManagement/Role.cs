using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;

namespace Techlabs.Euphoria.Kernel.Modules.SecurityManagement
{
    [Table("ROLE")]
    public class Role : MasterEntity
    {
        public string RoleName { get; set; }
        public Role()
        {
        }

        public Role(string roleName) : this()
        {
            Validate(roleName);
            this.RoleName = roleName;
        }

        private static void Validate(string roleName)
        {
            if (String.IsNullOrEmpty(roleName))
                throw new ValidationException("Invalid Role");
        }

        public static Role Create(string roleName)
        {
            return new Role(roleName);
        }

        public void Update(string roleName)
        {
            Validate(roleName);
            this.RoleName = roleName;
        }
    }
}
