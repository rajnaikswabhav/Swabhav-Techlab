using System;

namespace NumberGuesserGame
{
    public class GameConsole
    {
        private readonly int range;
        private readonly int attempts;
        private int guessNumber;
        private NumberGuesser numberGuesser;
        private static int count = 1;

        public GameConsole(int range, int attempts)
        {
            this.range = range;
            this.attempts = attempts;
            numberGuesser = new NumberGuesser();
            numberGuesser.IntializeGame(range);
        }

        public void Start()
        {
            Console.WriteLine("Guess Any Number....");
            guessNumber = int.Parse(Console.ReadLine());
           
            if(guessNumber > range)
            {
                Console.WriteLine("Please enter value only in range...");
                Start();
            }
            numberGuesser.Match(guessNumber);
            if (count == attempts)
            {

                Console.WriteLine("You are out of attempts...Please Try agin later....");
                Environment.Exit(0);   
            }

            else
            {
                Compare();
            }
        }

        public void Compare()
        {
            if (numberGuesser.GameStatus == GameStatus.END)
            {
                Console.WriteLine("You win the game.....");
                Environment.Exit(0);
            }
            else if(numberGuesser.GameStatus == GameStatus.INPROGRESS)
            {
                if(numberGuesser.PrivateNumber > guessNumber)
                {
                    Console.WriteLine("Private number is greatethan guess Number.....");
                }else
                {
                    Console.WriteLine("Private number is lessthan guess number.....");
                }
                count++;
            }
            Start();
        }
    }
}
