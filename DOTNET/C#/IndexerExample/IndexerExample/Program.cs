using System;
using System.Collections.Generic;
using System.Text;

namespace IndexerExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var simpleCollection = new SimpleCollection<String>();
            simpleCollection[0] = "Hello C#...";
            Console.WriteLine(simpleCollection[0]);
        }
    }
}
