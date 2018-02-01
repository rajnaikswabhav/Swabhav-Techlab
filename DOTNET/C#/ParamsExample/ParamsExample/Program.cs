using System;
using System.Collections.Generic;
using System.Text;

namespace ParamsExample
{
    class Program
    {

        static void Main(string[] args)
        {
            var x = 3;
            dynamic y=4;
            y = "Akash";
            Console.WriteLine("var is: "+x);
            UseParams(1,2,3,4);
            UseParams2(1,"a","test");

            UseParams2();

            int[] intArray = {5,6,7,8,9 };
            UseParams(intArray);

            object[] objectArray = { 2,"b","test","again"};
            UseParams2(objectArray);
            UseParams2(intArray);
        }

        public static void UseParams(params int[] list)
        {
            for (int i = 0; i < list.Length; i++)
            {
                Console.Write(list[i] + " ");
            }
            Console.WriteLine();
        }

        public static void UseParams2(params object[] list)
        {
            for (int i = 0; i < list.Length; i++)
            {
                Console.Write(list[i] + " ");
            }
            Console.WriteLine();
        }

    }

}
