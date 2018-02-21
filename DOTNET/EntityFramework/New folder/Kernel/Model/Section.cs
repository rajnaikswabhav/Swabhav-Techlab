using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;

namespace Techlabs.Euphoria.Kernel.Model
{
    [Table("SECTION")]
    public class Section : DimensionEntity
    {
        public string Name { get; set; }

        public virtual SectionType SectionType { get; set; }
        public virtual ICollection<Access> Accesses { get; set; }
        public virtual ICollection<Section> Sections { get; set; }
        public virtual ICollection<Stall> Stalls { get; set; }

        public Section()
        {
            Accesses = new List<Access>();
            Sections = new List<Section>();
            Stalls = new List<Stall>();
        }
    }
}
