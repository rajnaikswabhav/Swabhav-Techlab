using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techlabs.Euphoria.Kernel.Specification
{
    public class TicketDaysSpecification
    {
        public void GetAllDatesAndInitializeTickets(DateTime startDateOfExhibition, DateTime endDateOfExhibition)
        {
            List<DateTime> allAvailableDatesForBooking = new List<DateTime>();

            List<DateTime> allAvailableWeekendsForbooking = new List<DateTime>();

            DateTime currentValidStartDate;

            if (startDateOfExhibition <= DateTime.UtcNow)
                currentValidStartDate = DateTime.UtcNow;
            else
                currentValidStartDate = startDateOfExhibition;



            for (DateTime date = currentValidStartDate; date <= endDateOfExhibition; date = date.AddDays(1))
            {
                allAvailableDatesForBooking.Add(date);

                if (date.DayOfWeek == DayOfWeek.Saturday)
                {
                    allAvailableWeekendsForbooking.Add(date);
                }
            }
            //return (allAvailableDatesForBooking, allAvailableWeekendsForbooking);
        }

       
    }
}
