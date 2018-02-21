using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;

namespace Event_Management.Model
{
    [Table("EVENTTICKETTYPE")]
    public class EventTicketType : MasterEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }

        public virtual Event Event { get; set; }

        public EventTicketType()
        {

        }

        public EventTicketType(string name, string description,int cost ) : this()
        {
            Validate(name,description,cost);

            this.Name = name;
            this.Description = description;
            this.Cost = cost;
        }

        private static void Validate(string name, string description, int cost)
        {
            if (String.IsNullOrEmpty(name))
                throw new ValidationException("Invalid Ticket Name");

        }

        public static EventTicketType Create(string name, string description, int cost)
        {
            return new EventTicketType(name, description, cost);
        }

        public void Update(string name, string description, int cost)
        {
            Validate(name, description, cost);

            this.Name = name;
            this.Description = description;
            this.Cost = cost;
        }
    }
}
