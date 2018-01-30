using System;
using System.Collections.Generic;
using System.Text;

namespace PlayerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Case1();
            Case2();
        }

        private static void Case2()
        {
            Player player1 = new Player(101, "A", 20);
            Player player2 = new Player(101, "A", 20);
            Console.WriteLine(player1 == player2);
            Console.WriteLine(player1.Equals(player2));
        }

        private static void Case1()
        {
            Player sachin = new Player(10, "Sachin", 48);
            Player virat = new Player(1, "Virat");

            Console.WriteLine("Sachin's Id: " + sachin.Id);
            Console.WriteLine("Virat's Id: " + virat.Id);
            Console.WriteLine("Name :" + sachin.Name);
            Console.WriteLine("Age :" + virat.Age);

            Player elderPlayer = sachin.WhoIsElder(virat);
            Console.WriteLine("Elder Player is: " + elderPlayer.Name + "\n");
            Console.WriteLine(sachin);
            Console.WriteLine(virat.ToString());
        }
    }
}
