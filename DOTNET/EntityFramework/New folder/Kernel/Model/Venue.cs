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
    [Table("VENUE")]
    public class Venue :MasterEntity
    {
        public string City { get; set; }
        public String Address { get; set; }
        public String State { get; set; }
        public int Order { get; set; }

        public virtual Organizer Organizer { get; set; }

        public virtual ICollection<ExhibitorFeedback> ExhibitorFeedback { get; set; }
        public virtual ICollection<TicketType> TicketTypes { get; set; }
        public virtual ICollection<Exhibition> Exhibitions { get; set; }

        public Venue() {
            TicketTypes = new List<TicketType>();
            Exhibitions = new List<Exhibition>();
            ExhibitorFeedback = new List<ExhibitorFeedback>();
        }

        private Venue(string city, string address, String state,int order=0) {

            Validate(city, address, state);
            this.City = city;
            this.Address = address;
            this.State = state;
            this.Order = 0;
        }

        private void Validate(string city, string address, string state)
        {
            if (String.IsNullOrEmpty(city))
                throw new ValidationException("Invalid City");
            if (String.IsNullOrEmpty(address))
                throw new ValidationException("Invalid Address");
            if (String.IsNullOrEmpty(state))
                throw new ValidationException("Invalid State");
        }

        public static Venue Create(String city, String address, String state) {

            return new Venue(city, address, state);
        }

        public void Update(string city, string address, string state)
        {
            Validate(city, address, state);
            this.City = city;
            this.Address = address;
            this.State = state;    
        }
    }
}
