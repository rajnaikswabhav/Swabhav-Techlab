using System;
using System.Collections.Generic;
using System.Text;

namespace FuncActionApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Predicate<int> isEvenV1;
            isEvenV1 = IsAnEvenNumber;
            Console.WriteLine(isEvenV1(10));

            Predicate<int> isEvenV2 = (n) => n % 2 == 0;
            Console.WriteLine(isEvenV2(5));

            Func<int, long> cubeEvenV1 = CubeEvenNumber;
            Console.WriteLine(cubeEvenV1(3));

            Func<int, long> cubeEvenV2 = (n) => {
                if (isEvenV1(n))
                {
                    return n * n * n;
                }
                else return 0;
            };
            Console.WriteLine(cubeEvenV2(4));

            Action<int> printEvenV1 = PrintEvenNumberTill;
            printEvenV1(10);

            Action<int> printEventV2 = (n) =>
            {
                for (int i = 1; i <= n; i++)
                {
                    if (i % 2 == 0) { Console.Write("{0},",i); }
                }
            };
            printEventV2(5);
            Console.WriteLine();
        }

        private static bool IsAnEvenNumber(int number)
        {
            return  (number % 2 == 0) ;
        }

        private static long CubeEvenNumber(int number)
        {
            if (IsAnEvenNumber(number))
            {
                return number * number * number;
            }
            else
            {
                return 0;
            }
        }

        private static void PrintEvenNumberTill(int number) {
            for(int i=1; i<=number; i++)
            {
                if (i % 2 == 0)
                {
                    Console.Write("{0},", i);
                }
            }
            Console.WriteLine();
        }
    }
}
