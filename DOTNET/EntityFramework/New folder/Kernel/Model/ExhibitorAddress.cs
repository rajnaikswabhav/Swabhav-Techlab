using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;

namespace Techlabs.Euphoria.Kernel.Model
{
    [Table("EXHIBITORADDRESS")]
    public class ExhibitorAddress : MasterEntity
    {
        public string Type { get; set; }
        public string Address { get; set; }

        public virtual Exhibitor Exhibitor { get; set; }
    }
}
