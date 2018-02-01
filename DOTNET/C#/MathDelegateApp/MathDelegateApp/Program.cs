using System;
using System.Collections.Generic;
using System.Text;

namespace MathDelegateApp
{
    delegate void DCalculateNumbers(int value1, int value2);
    class Program
    {
        static void Main(string[] args)
        {
            DCalculateNumbers number;
            number = AddNumbers;
            number += SubstractNumbers;
            number += MultiplyNumbers;
            number += DivisionNumbers;

            number(10, 5);
        }

        private static void AddNumbers(int value1, int value2)
        {
            var sum = value1 + value2;
            Console.WriteLine("Sum of {0} and {1} is: {2}", value1, value2, sum);
        }

        private static void SubstractNumbers(int value1, int value2)
        {
            var sub = value1 - value2;
            Console.WriteLine("Substraction of {0} and {1} is: {2}", value1, value2, sub);
        }

        private static void MultiplyNumbers(int value1, int value2)
        {
            var mul = value1 * value2;
            Console.WriteLine("Multiplication of {0} and {1} is: {2}", value1, value2, mul);
        }

        private static void DivisionNumbers(int value1, int value2)
        {
            var div = value1 / value2;
            Console.WriteLine("Division of {0} and {1} is: {2}", value1, value2, div);
        }
    }
}
