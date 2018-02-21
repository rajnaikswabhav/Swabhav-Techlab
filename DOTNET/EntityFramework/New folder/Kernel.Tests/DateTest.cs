using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernel.Tests
{
     class DateTest
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

                if (date.DayOfWeek== DayOfWeek.Saturday)
                {
                    allAvailableWeekendsForbooking.Add(date);
                }
            }

            allAvailableWeekendsForbooking.ForEach((x) => Console.WriteLine(x.Date + "- "+x.DayOfWeek));
            Console.WriteLine("\n\n\n======");
            allAvailableDatesForBooking.ForEach((x) => Console.WriteLine(x.Date + "- " + x.DayOfWeek));

        }


        public static void _Main()
        {
            new DateTest().GetAllDatesAndInitializeTickets(new DateTime(2016, 1, 22), new DateTime(2016, 1, 31));
            Console.ReadLine();

            
        }
    }
}
