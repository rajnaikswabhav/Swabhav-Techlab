using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernel.Tests.EnumTypeofTicket
{
    public enum TypeOfTicket
    {

        AllDays = 10, Single = 1, Weekend = 2
    }


    class TestTypeOfTicket
    {

        public static void _Main() {

            Console.WriteLine((int)TypeOfTicket.AllDays);
            Console.WriteLine((int)TypeOfTicket.Single);
            Console.WriteLine((int)TypeOfTicket.Weekend);

            Console.ReadLine();
        }
    }
}
