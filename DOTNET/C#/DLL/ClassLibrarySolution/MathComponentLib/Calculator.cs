using System;
using System.Collections.Generic;
using System.Text;

namespace MathComponentLib
{
    public class Calculator
    {
        private bool IsEven(int number)
        {
            if (number % 2 == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int CubeEvenNumbers(int number)
        {
            if (IsEven(number))
            {
                return number * number * number;
            }
            else
            {
                throw new Exception("Number is not Even...");
            }
        }
    }
}
