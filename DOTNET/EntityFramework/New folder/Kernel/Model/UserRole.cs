using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;

namespace Techlabs.Euphoria.Kernel.Model
{
    [Table("USERROLE")]
    public class UserRole : MasterEntity
    {
        public string Name { get; set; }
        public ICollection<User> Users { get; set; }
        public UserRole() { }
    }
}
