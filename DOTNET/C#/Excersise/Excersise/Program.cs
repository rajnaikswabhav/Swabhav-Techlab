using System;
using System.Collections.Generic;
using System.Text;

namespace Excersise
{
    class Program
    {
        
        static void Main(string[] args)
        {
            int value = 1;
            Method(ref value);
            Console.WriteLine("Value: "+value);
        }

        static void  Method(ref int number)
        {
            number = 10;
        }  
    }
}
