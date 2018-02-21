using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;

namespace Techlabs.Euphoria.Kernel.Modules.MailManagement
{
    [Table("EMAILTYPE")]
    public class EmailType : MasterEntity
    {
        public string Type { get; set; }

        public EmailType()
        {
        }

        public EmailType(string type) : this()
        {
            Validate(type);

            this.Type = type;
        }

        private static void Validate(string type)
        {
            if (string.IsNullOrEmpty(type))
                throw new ValidationException("Invalid Type");
        }

        public static EmailType Create(string type)
        {
            return new EmailType(type);
        }

        public void Update(string type)
        {
            Validate(type);

            this.Type = type;
        }
    }
}
