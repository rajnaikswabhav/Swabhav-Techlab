using MathComponentLib;
using MathComponentLib.Techlabs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var calculator = new Calculator();
            Console.WriteLine(calculator.CubeEvenNumbers(4));

            var dto = new StudentDTO
            {
                Location = "Mumbai",
                Name = "Akash"
            };

            var dto2 = new StudentDTO
            {
                Location = "Mumbai",
                Name = "Brijesh"
            };

            var dto3 = new StudentDTO
            {
                Location = "Mumbai",
                Name = "Raaj"
            };

            Console.WriteLine(dto.Id);
            Console.WriteLine(dto.ToString());
            Console.WriteLine(dto2);
            Console.WriteLine(dto3);
            Console.WriteLine(dto.Id);
            Console.WriteLine(dto.GetType());
            Console.WriteLine(dto.GetHashCode());
            Console.WriteLine(dto2.GetHashCode());
        }
    }
}
