using System;
using System.Collections.Generic;
using System.Text;

namespace InterfacePolymorphismApp
{
    public class Boy : IMAnneravle, IEMotionable
    {
        public void Cry()
        {
            Console.WriteLine("Boy is crying....");
        }

        public void Depart()
        {
            Console.WriteLine("Boy is upto depart.....");
        }

        public void Laugh()
        {
            Console.WriteLine("Boy is lauging.....");
        }

        public void Wish()
        {
            Console.WriteLine("Boy is wishing....");
        }
    }
}
