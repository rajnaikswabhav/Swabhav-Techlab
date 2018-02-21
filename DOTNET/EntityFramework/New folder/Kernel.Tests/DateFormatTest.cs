using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernel.Tests
{
    class DateFormatTest
    {
        public static void _Main() {

           // Console.WriteLine(DateTime.UtcNow.ToString("dd-MMM-yyyy"));

            var date = "25-Jan-2016";
           DateTime convertedDate = DateTime.ParseExact(date, "dd-MMM-yyyy", CultureInfo.InvariantCulture);
           Console.WriteLine(convertedDate);

            Console.WriteLine(Convert.ToDateTime("25-Jan-2016"));
            Console.WriteLine(Convert.ToDateTime("25-Jan-2016").ToString("dd-MMM-yyyy"));

            Console.ReadKey();

        }
    }
}
