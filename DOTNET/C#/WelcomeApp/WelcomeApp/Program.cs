using System;
using System.Collections.Generic;
using System.Linq;

namespace WelcomeApp
{
    class Program
    {
        static void Main(string[] names)
        {
            //Case1(names);
            // Case2();
            //Case3();
            IEnumerable<String> nameStartWithA = names.Where((n) => n.StartsWith("A"));

            foreach (var name in nameStartWithA)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine(name);
            }

            IEnumerable<String> nameStartWithAAndContainS = nameStartWithA.Where((n) => n.Contains("s"))
                                                                          .Take(2);
            Console.WriteLine();
            foreach (var name in nameStartWithAAndContainS)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(name);
            }
            
        }

        private static void Case3()
        {
            Rectangle rectangle = new Rectangle(10, 15, BorderStyle.DOUBLE);
            Rectangle rectangle2 = new Rectangle(15, 20, BorderStyle.DOTTED);
            Rectangle rectangle3 = new Rectangle(20, 25, BorderStyle.SINGLE);

            Rectangle[] rectangles = new Rectangle[] { rectangle, rectangle2, rectangle3 };

            foreach (Rectangle r in rectangles)
            {
                Console.WriteLine("Area of Rectangle is: " + r.Area);
                Console.WriteLine("BroderStyle of Rectangle is:" + r.BorderStyle);
            }
        }

        private static void Case2()
        {
            Rectangle rectangle = new Rectangle(5, 10, BorderStyle.DOUBLE);
            rectangle.Width = 20;
            rectangle.Height = 10;
            Console.WriteLine("BroderStyle:" + rectangle.BorderStyle);
            Console.WriteLine("Area of Rectangle :" + rectangle.Area);
        }

        private static void Case1(string[] names)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(names.Length);
            foreach (String name in names)
            {
                Console.WriteLine(name);
            }
        }
    }
}
