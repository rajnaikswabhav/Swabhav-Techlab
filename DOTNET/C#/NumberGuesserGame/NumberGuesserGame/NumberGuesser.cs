using System;

namespace NumberGuesserGame
{
    public enum GameStatus
    {
        START, END, INPROGRESS
    }

    public enum NumberStatus
    {
        LESSTHAN, GREATERTHAN, MATCH
    }
    public class NumberGuesser
    {
        private int privateNumber;
        private GameStatus gameStatus;

        public void IntializeGame(int range)
        {
            Random random = new Random();
            privateNumber = random.Next(1, range);

            Console.WriteLine("Private number is between 1 to {0}", range);
            Console.WriteLine("Private number is: {0}", privateNumber);
        }

        public void Match(int number)
        {
            if (privateNumber == number)
            {
                gameStatus = GameStatus.END;
            }
            else
            {
                gameStatus = GameStatus.INPROGRESS;
            }
        }

        public int PrivateNumber {get { return privateNumber; } }

        public GameStatus GameStatus
        {
            get
            {
                return gameStatus;
            }
            set
            {
                gameStatus = value;
            }
        }
    }
}
