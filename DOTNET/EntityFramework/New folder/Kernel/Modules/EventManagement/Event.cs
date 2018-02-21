using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;
using Techlabs.Euphoria.Kernel.Model;

namespace Modules.EventManagement
{
    [Table("EVENT")]
    public class Event : MasterEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime BookingStartDate { get; set; }
        public bool isActive { get; set; }
        public bool IsTicketingActive { get; set; }
        public string Time { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public virtual Venue Venue { get; set; }
        public Event()
        {
        }

        public Event(string name, string address, DateTime startDate, DateTime endDate, DateTime bookingStartDate, bool isActive, string time, bool isTicketingActive) : this()
        {
            Validate(name, address, startDate, endDate, bookingStartDate, time);

            this.Name = name;
            this.Address = address;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.BookingStartDate = bookingStartDate;
            this.isActive = isActive;
            this.Time = time;
            this.IsTicketingActive = isTicketingActive;
        }

        private static void Validate(string name, string address, DateTime startDate, DateTime endDate, DateTime bookingStartDate, string time)
        {
            if (String.IsNullOrEmpty(name))
                throw new ValidationException("Invalid Category Name");

        }

        public static Event Create(string name, string address, DateTime startDate, DateTime endDate, DateTime bookingStartDate, bool isActive, string time, bool isTicketingActive)
        {
            Validate(name, address, startDate, endDate, bookingStartDate, time);
            return new Event(name, address, startDate, endDate, bookingStartDate, isActive, time, isTicketingActive);
        }

        public void Update(string name, string address, DateTime startDate, DateTime endDate, DateTime bookingStartDate, bool isActive, string time, bool isTicketingActive)
        {
            Validate(name, address, startDate, endDate, bookingStartDate, time);

            this.Name = name;
            this.Address = address;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.BookingStartDate = bookingStartDate;
            this.isActive = isActive;
            this.Time = time;
            this.IsTicketingActive = isTicketingActive;
        }
    }
}
