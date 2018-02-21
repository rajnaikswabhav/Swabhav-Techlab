using Modules.EventManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;

namespace Techlabs.Euphoria.Kernel.Modules.BookingManagement
{
    [Table("BOOKINGEXHIBITORDISCOUNT")]
    public class BookingExhibitorDiscount : MasterEntity
    {
        public string Heading { get; set; }
        public string Description { get; set; }
        public bool IsApproved { get; set; }
        public bool IsRejected { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ClickCount { get; set; }
        public string BgImage { get; set; }

        public virtual Booking Booking { get; set; }

        public BookingExhibitorDiscount()
        {
        }

        private BookingExhibitorDiscount(string heading, string description, bool isApproved, bool isRejected, DateTime startDate, DateTime endDate, string bgImage)
        {
            Validate(heading, description, isApproved, isRejected, startDate, endDate, bgImage);
            this.Heading = heading;
            this.Description = description;
            this.IsApproved = isApproved;
            this.IsRejected = isRejected;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.BgImage = bgImage;
        }

        private static void Validate(string heading, string description, bool isApproved, bool isRejected, DateTime startDate, DateTime endDate, string bgImage)
        {
            if (String.IsNullOrEmpty(heading))
                throw new ValidationException("Invalid Heading");

            if (String.IsNullOrEmpty(description))
                throw new ValidationException("Invalid Description");
        }

        public static BookingExhibitorDiscount Create(string heading, string description, bool isApproved, bool isRejected, DateTime startDate, DateTime endDate, string bgImage)
        {
            return new BookingExhibitorDiscount(heading, description, isApproved, isRejected, startDate, endDate, bgImage);
        }

        public void Update(string heading, string description, bool isApproved, bool isRejected, DateTime startDate, DateTime endDate, string bgImage)
        {
            Validate(heading, description, isApproved, isRejected, startDate, endDate, bgImage);
            this.Heading = heading;
            this.Description = description;
            this.IsApproved = isApproved;
            this.IsRejected = isRejected;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.BgImage = bgImage;
        }
    }
}
