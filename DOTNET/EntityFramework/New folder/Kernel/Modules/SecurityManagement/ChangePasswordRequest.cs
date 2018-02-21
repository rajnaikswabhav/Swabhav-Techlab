using Modules.SecurityManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;

namespace Techlabs.Euphoria.Kernel.Modules.SecurityManagement
{
    [Table("CHANGEPASSWORDREQUEST")]
    public class ChangePasswordRequest : MasterEntity
    {
        public virtual Login Login { get; set; }
        public virtual Role Role { get; set; }     
    }
}
