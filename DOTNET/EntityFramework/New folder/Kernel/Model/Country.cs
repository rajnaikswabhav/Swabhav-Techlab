using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;

namespace Techlabs.Euphoria.Kernel.Model
{
    [Table("COUNTRY")]
    public class Country : MasterEntity
    {
        public string Name { get; set; }
        public string Color { get; set; }

        public virtual Exhibition Exhibitons { get; set; }
        public virtual ICollection<VisitorFeedback> VisitorFeedback { get; set; }

        public Country()
        {
            VisitorFeedback = new List<VisitorFeedback>();
        }

        public Country(string name) : this()
        {
            Validate(name);
            this.Name = name;
        }

        private static void Validate(string name)
        {
            if (String.IsNullOrEmpty(name))
                throw new ValidationException("Invalid Category Name");
        }

        public static Country Create(string name)
        {
            return new Country(name);
        }

        public void Update(string name, string color)
        {
            Validate(name);
            this.Name = name;
            this.Color = color;
        }
    }
}
