using System;
using System.Collections.Generic;
using System.Text;

namespace OutExample
{
    class Program
    {
        static void Method(out int number, out string name)
        {
            number = 44;
            name = "Akash";
          
        }
        static void Main(string[] args)
        {
            int value;
            string str1;
            Method(out value, out str1);
            Console.WriteLine(value);
            Console.WriteLine(str1);
        }
    }
}
