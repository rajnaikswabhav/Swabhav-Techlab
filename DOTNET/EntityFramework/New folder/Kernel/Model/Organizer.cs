using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;
using Techlabs.Euphoria.Kernel.Model;

namespace Techlabs.Euphoria.Kernel.Model
{
    [Table("ORGANIZER")]
    public class Organizer : MasterEntity
    {
        [Key]
        [Column(Order = 2)]
        public int TenantId { get;  set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public virtual ICollection<Exhibition> Exhibitions { get; set; }

        public virtual ICollection<Visitor> Visitors { get; set; }

        public virtual ICollection<Exhibitor> Exhibitors { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

        public virtual ICollection<Venue> Venues { get; set; }

        public Organizer() 
        {
            Exhibitions = new List<Exhibition>();
            Visitors = new List<Visitor>();
            Exhibitors = new List<Exhibitor>();
            Categories = new List<Category>();
            Venues = new List<Venue>();
        }

        private Organizer(int tenantId, string name, string description) : this()
        {
            Validate(name, description);

            TenantId = tenantId;
            Name = name;
            Description = description;
        }

        private static void Validate(string name, string description)
        {
            if (String.IsNullOrEmpty(name))
                throw new ValidationException("Invalid Name");
        }

        public void Update(string name, string description)
        {
            Validate(name, description);
          
            Name = name;
            Description = description;
        }

        public static Organizer Create(int tenantID ,string name, string description)
        {
            return new Organizer( tenantID,name, description);
        }
    }
} 
