using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;

namespace Techlabs.Euphoria.Kernel.Model
{
    [Table("SECTIONTYPE")]
    public class SectionType : MasterEntity
    {
        public string Name { get; set; }

        public SectionType()
        {
        }
    }
}
