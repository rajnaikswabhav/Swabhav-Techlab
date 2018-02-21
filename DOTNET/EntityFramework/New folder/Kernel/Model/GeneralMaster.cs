using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;

namespace Techlabs.Euphoria.Kernel.Model
{
    [Table("GENERALMASTER")]
    public class GeneralMaster : MasterEntity
    {
        public string Type { get; set; }
        public string Value { get; set; }
        public int Key { get; set; }
        public bool Active { get; set; }

        public GeneralMaster()
        {
        }

        public GeneralMaster(string type, string value, int key, bool active) : this()
        {
            Validate(type, value, key, active);

            this.Type = type;
            this.Value = value;
            this.Key = key;
            this.Active = active;
        }

        private static void Validate(string type, string value, int key, bool active)
        {
            //if (String.IsNullOrEmpty(name))
            //    throw new ValidationException("Invalid Category Name");
        }

        public static GeneralMaster Create(string type, string value, int key, bool active)
        {
            return new GeneralMaster(type, value, key, active);
        }

        public void Update(string type, string value, int key, bool active)
        {
            Validate(type, value, key, active);

            this.Type = type;
            this.Value = value;
            this.Key = key;
            this.Active = active;
        }
    }
}
