using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techlabs.Euphoria.Kernel.Specification
{
    public class LoginSearchCriteria
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public Guid RoleId { get; set; }
        public string Role { get; set; }
    }
}
