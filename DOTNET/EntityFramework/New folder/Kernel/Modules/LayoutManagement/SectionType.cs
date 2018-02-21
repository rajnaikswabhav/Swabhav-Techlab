using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;

namespace Modules.LayoutManagement
{
    [Table("SECTIONTYPE")]
    public class SectionType : MasterEntity
    {
        public string Name { get; set; }

        public SectionType()
        {
        }

        public SectionType(string name) : this()
        {
            Validate(name);

            this.Name = name;      
        }

        private static void Validate(string name)
        {
            if (String.IsNullOrEmpty(name))
                throw new ValidationException("Invalid Name");
        }

        public static SectionType Create(string name)
        {
            return new SectionType(name);
        }

        public void Update(string name, string description)
        {
            Validate(name);
            this.Name = name;
        }
    }
}
