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
    [Table("EXHIBITION")]
    public class Exhibition : MasterEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool isActive { get; set; }
        public string BannerImage { get; set; }
        public string Bannertext { get; set; }
        public string Logo { get; set; }
        public string BgImage { get; set; }
        public string ExhibitionTime { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public virtual TicketBookingStatus TicketBookingStatus { get; set; }

        public virtual Organizer Organizer { get; set; }

        public virtual Venue Venue { get; set; }

        public virtual ICollection<Exhibitor> Exhibitors { get; set; }

        public virtual ICollection<TicketType> TicketTypes { get; set; }

        public virtual ICollection<Pavilion> Pavilions { get; set;}

        public virtual ICollection<Country> Countries { get; set; }

        public virtual ICollection<State> States { get; set; }

        public Exhibition()
        {
            TicketTypes = new List<TicketType>();
            Pavilions = new List<Pavilion>();
            Exhibitors = new List<Exhibitor>();
            Countries = new List<Country>();
            States = new List<State>();
        }

        private Exhibition(string name, DateTime startDate, DateTime endDate, string address, string description, bool isActive, string exhibitionTime, string latitude, string longitude)
        {
            Validate(name, startDate, EndDate, description,isActive);

            this.Name = name;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Address = address;
            this.Description = description;
            this.isActive = isActive;
            this.ExhibitionTime = exhibitionTime;
            this.Latitude = latitude;
            this.Longitude = Longitude;
        }

        public void Validate(string name, DateTime startDate, DateTime endDate, string description,bool isActive)
        {
            if (string.IsNullOrEmpty(name))
                throw new ValidationException("Invalid Name");
            if (string.IsNullOrEmpty(description))
                throw new ValidationException("Invalid Description");
        }

        public static Exhibition Create(string name, DateTime startDate, DateTime endDate, string address, string description, bool isActive, string exhibitionTime, string latitude, string longitude)
        {
            return new Exhibition(name, startDate, endDate, address, description, isActive, exhibitionTime, latitude, longitude);        
        }

        public void Update(string name, DateTime startDate, DateTime endDate, string address, string description, bool isActive, string exhibitionTime, string latitude, string longitude)
        {
            Validate(name, startDate, endDate, description, isActive);
            this.Name = name;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Address = address;
            this.Description = description;
            this.isActive = isActive;
            this.ExhibitionTime = exhibitionTime;
            this.Longitude = longitude;
            this.Latitude = latitude;          
        }
    }
}
