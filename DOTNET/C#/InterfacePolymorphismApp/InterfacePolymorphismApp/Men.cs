using System;
using System.Collections.Generic;
using System.Text;

namespace InterfacePolymorphismApp
{
    public class Men : IMAnneravle
    {
        public void Depart()
        {
            Console.WriteLine("Men is upto Depart.......");
        }

        public void Wish()
        {
            Console.WriteLine("Men is wishing......");
        }
    }
}
