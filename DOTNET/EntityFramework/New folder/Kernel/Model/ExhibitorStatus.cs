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
    [Table("EXHIBITORSTATUS")]
    public class ExhibitorStatus : MasterEntity
    {
        public string Status { get; set; }

        public ExhibitorStatus()
        {
        }

        public ExhibitorStatus(string status) : this()
        {
            Validate(status);
            this.Status = status;
        }

        private static void Validate(string status)
        {
            if (String.IsNullOrEmpty(status))
                throw new ValidationException("Invalid Status Name");
        }

        public static ExhibitorStatus Create(string status)
        {
            return new ExhibitorStatus(status);
        }

        public void Update(string status)
        {
            Validate(status);
            this.Status = status;
        }
    }
}
