using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Techlabs.Euphoria.Kernel.Framework.Model;
using Techlabs.Euphoria.Kernel.Model;

namespace Techlabs.Euphoria.Kernel.Model
{
    [Table("TICKETTYPE")]
    public class TicketType : MasterEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int BusinessHrs { get; set; }
        public int NonBusinessHrs { get; set; }
        public int BusinessHrsDiscount { get; set; }
        public int NonBusinessHrsDiscount { get; set; }
        public int NumberOfDaysIncluded { get; set; }

        public virtual Venue Venue { get; set; }

        public virtual ICollection<Exhibition> Exhibitions { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }

        public TicketType()
        {
            Exhibitions = new List<Exhibition>();
        }

        private TicketType(string name, int businessHrs, int nonBusinessHrs, string description)
        {
            Validate(name, nonBusinessHrs, description);

            this.Name = name;
            this.BusinessHrs = businessHrs;
            this.NonBusinessHrs = nonBusinessHrs;
            this.Description = description;
        }

        private static void Validate(string name, int nonBusinessHrs, string description)
        {
            if (String.IsNullOrEmpty(description))
                throw new ValidationException("Invalid Type");
            if (String.IsNullOrEmpty(name))
                throw new ValidationException("Invalid Type");
        }

        public static TicketType Create(string name, int businessHrs, int nonBusinessHrs, string description)
        {
            return new TicketType(name, businessHrs, nonBusinessHrs, description);
        }

        public void Update(string name, int businessHrs, int nonBusinessHrs, string description)
        {
            Validate(name, nonBusinessHrs, description);

            this.Name = name;
            this.BusinessHrs = businessHrs;
            this.NonBusinessHrs = nonBusinessHrs;
            this.Description = description;
        }
    }
}
