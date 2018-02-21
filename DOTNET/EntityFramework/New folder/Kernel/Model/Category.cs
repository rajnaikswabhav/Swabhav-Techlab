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
    [Table("CATEGORY")]
    public class Category : MasterEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual Organizer Organizer { get; set; }

        public virtual ICollection<Exhibitor> Exhibitor { get; set; }
        public virtual ICollection<VisitorFeedback> VisitorFeedback { get; set; }
        public virtual ICollection<Visitor> Visitors { get; set; }
        public Category()
        {
            Visitors = new List<Visitor>();
            Exhibitor = new List<Exhibitor>();
            VisitorFeedback = new List<VisitorFeedback>();
        }

        public Category(string name, string description) : this()
        {
            Validate(name, description);

            this.Name = name;
            Description = description;
        }

        private static void Validate(string name, string description)
        {
            if (String.IsNullOrEmpty(name))
                throw new ValidationException("Invalid Category Name");
            if (String.IsNullOrEmpty(description))
                throw new ValidationException("Invalid Category Description");
        }

        public static Category Create(string name, string description)
        {
            return new Category(name, description);
        }

        public void Update(string name, string description)
        {
            Validate(name, description);

            this.Name = name;
            Description = description;
        }
    }
}
