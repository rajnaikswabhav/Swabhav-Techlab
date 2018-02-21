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
    [Table("EXHIBITORTYPE")]
    public class ExhibitorType : MasterEntity
    {
        public string Type { get; set; }
        public virtual ICollection<VisitorFeedback> VisitorFeedback { get; set; }
        public ExhibitorType()
        {
            VisitorFeedback = new List<VisitorFeedback>();
        }



        public ExhibitorType(string type) : this()
        {
            Validate(type);
            this.Type = type;

        }

        private static void Validate(string type)
        {
            if (String.IsNullOrEmpty(type))
                throw new ValidationException("Invalid Exhibitor Type");
        }

        public static ExhibitorType Create(string type)
        {
            return new ExhibitorType(type);
        }

        public void Update(string type)
        {
            Validate(type);
            this.Type = type;
        }
    }
}
