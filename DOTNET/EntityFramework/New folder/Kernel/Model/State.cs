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
    [Table("STATE")]
    public class State : MasterEntity
    {
        public string Name { get; set; }
        public string Color { get; set; }

        public virtual Exhibition Exhibitons { get; set; }

        public State()
        {
        }

        public State(string name) : this()
        {
            Validate(name);
            this.Name = name;
        }

        private static void Validate(string name)
        {
            if (String.IsNullOrEmpty(name))
                throw new ValidationException("Invalid Category Name");
        }

        public static State Create(string name)
        {
            return new State(name);
        }

        public void Update(string name, string color)
        {
            Validate(name);
            this.Name = name;
            this.Color = color;
        }
    }
}
