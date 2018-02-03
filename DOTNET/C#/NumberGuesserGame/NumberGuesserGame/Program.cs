using System;
using System.Collections.Generic;
using System.Text;

namespace NumberGuesserGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var console = new GameConsole(100,3);
            console.Start();
        }
    }
}
