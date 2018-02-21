using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;

namespace Techlabs.Euphoria.Kernel.Model
{
    [Table("ACCESS")]
    public class Access : DimensionEntity
    {
        public bool IsEntry { get; set; }
        public bool IsExit { get; set; }
        public bool IsEmergencyExit { get; set; }

        public virtual Section Section { get; set; }

        public Access()
        {
        }
    }
}
