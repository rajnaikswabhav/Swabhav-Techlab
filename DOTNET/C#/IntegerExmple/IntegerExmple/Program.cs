using System;
using System.Collections.Generic;
using System.Text;

namespace IntegerExmple
{
    class Program
    {
        static void Main(string[] args)
        {
            String value = "100";
            int number = int.Parse(value);
            int result;
            Console.WriteLine(number);
            bool isParse = int.TryParse(value,out result);
            Console.WriteLine("{0},{1}",isParse,result);
            int parseResult = Convert.ToInt32(value);
            Console.WriteLine(parseResult);
        }
    }
}
