using Modules.LayoutManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Techlabs.Euphoria.Kernel.Framework.Model;

namespace Techlabs.Euphoria.Kernel.Model
{
    [Table("PAVILION")]
    public class Pavilion : MasterEntity
    {
        public string Name { get; set; }

        public virtual Exhibition Exhibition { get; set;}

        public virtual ICollection<Stall> Stalls { get; set; }

        public Pavilion()
        {
            Stalls = new List<Stall>();
        }

        public Pavilion(string name)
        {
            Validate(name);
            this.Name = name;
        }

        private static void Validate(string name)
        {
            if (String.IsNullOrEmpty(name))
                throw new ValidationException("Invalid Pavilion Name");
        }

        public static Pavilion Create(string name)
        {
            return new Pavilion(name);
        }

        public void Update(string name)
        {
            Validate(name);
            this.Name = name;
        }
    }
}
