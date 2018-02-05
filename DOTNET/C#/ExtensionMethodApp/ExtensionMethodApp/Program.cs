using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtensionMethodApp
{
    class Program 
    {
       
        static void Main(string[] args)
        {
            string name = "Hello";
            Console.WriteLine(name.AddHitech());
            Console.WriteLine(name);  
        }
    }
}
