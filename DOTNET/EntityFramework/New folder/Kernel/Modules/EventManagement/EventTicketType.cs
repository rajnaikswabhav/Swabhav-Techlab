using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;

namespace Modules.EventManagement
{
    [Table("EVENTTICKETTYPE")]
    public class EventTicketType : MasterEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? BusinessHrs { get; set; }
        public int NonBusinessHrs { get; set; }
        public int? BusinessHrsDiscount { get; set; }
        public int NonBusinessHrsDiscount { get; set; }
        public int? NumberOfDaysIncluded { get; set; }

        public virtual Event Event { get; set; }

        public EventTicketType()
        {
        }

        public EventTicketType(string name, int? businessHrs, int nonBusinessHrs, int? businessHrsDiscount, int nonBusinessHrsDiscount, string description) : this()
        {
            Validate(name, businessHrs, nonBusinessHrs, businessHrsDiscount, nonBusinessHrsDiscount, description);

            this.Name = name;
            this.BusinessHrs = businessHrs;
            this.NonBusinessHrs = nonBusinessHrs;
            this.BusinessHrsDiscount = businessHrsDiscount;
            this.NonBusinessHrsDiscount = nonBusinessHrsDiscount;
            this.Description = description;
        }

        private static void Validate(string name, int? businessHrs, int nonBusinessHrs, int? businessHrsDiscount, int nonBusinessHrsDiscount, string description)
        {
            if (String.IsNullOrEmpty(name))
                throw new ValidationException("Invalid Ticket Name");
        }

        public static EventTicketType Create(string name, int? businessHrs, int nonBusinessHrs, int? businessHrsDiscount, int nonBusinessHrsDiscount, string description)
        {
            Validate(name, businessHrs, nonBusinessHrs, businessHrsDiscount, nonBusinessHrsDiscount, description);
            return new EventTicketType(name, businessHrs, nonBusinessHrs, businessHrsDiscount, nonBusinessHrsDiscount, description);
        }

        public void Update(string name, int businessHrs, int nonBusinessHrs, int businessHrsDiscount, int nonBusinessHrsDiscount, string description)
        {
            Validate(name, businessHrs, nonBusinessHrs, businessHrsDiscount, nonBusinessHrsDiscount, description);

            this.Name = name;
            this.BusinessHrs = businessHrs;
            this.NonBusinessHrs = nonBusinessHrs;
            this.BusinessHrsDiscount = businessHrsDiscount;
            this.NonBusinessHrsDiscount = nonBusinessHrsDiscount;
            this.Description = description;
        }
    }
}
