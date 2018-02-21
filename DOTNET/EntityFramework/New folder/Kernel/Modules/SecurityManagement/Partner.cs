using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;

namespace Techlabs.Euphoria.Kernel.Modules.SecurityManagement
{
    [Table("PARTNER")]
    public class Partner : MasterEntity
    {
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string PhoneNo { get; set; }
        public string Color { get; set; }
        public Partner()
        {
        }

        public Partner(string name, string emailId, string phoneNo, string color) : this()
        {
            Validate(name, emailId, phoneNo, color);

            this.Name = name;
            this.EmailId = emailId;
            this.PhoneNo = phoneNo;
            this.Color = color;
        }

        private static void Validate(string name, string emailId, string phoneNo, string color)
        {
            if (String.IsNullOrEmpty(name))
                throw new ValidationException("Invalid Name");
        }

        public static Partner Create(string name, string emailId, string phoneNo, string color)
        {
            return new Partner(name, emailId, phoneNo, color);
        }

        public void Update(string name, string emailId, string phoneNo, string color)
        {
            Validate(name, emailId, phoneNo, color);
            this.Name = name;
            this.EmailId = emailId;
            this.PhoneNo = phoneNo;
            this.Color = color;
        }
    }
}
