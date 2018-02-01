using System;
using System.Collections.Generic;
using System.Text;

namespace Excersise
{
    class OutExample
    {
        public static void Method(out int number, out string name)
        {
            number = 10;
            name = "Name";
        }

        public static void Main()
        {
            int value;
            string string1;
            Method(out value, out string1);
            Console.WriteLine(value);
            Console.WriteLine(string1);
        }
    }
}
