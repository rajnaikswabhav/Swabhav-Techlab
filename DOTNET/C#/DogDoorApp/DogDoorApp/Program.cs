using System;
using System.Threading;

namespace DogDoorApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            var door = new DogDoor();
            door.AddAllowedBark(new Bark("rowlf"));
            door.AddAllowedBark(new Bark("rooowlf"));
            door.AddAllowedBark(new Bark("rawlf"));
            door.AddAllowedBark(new Bark("woof"));

            var recognizer = new BarkRecognizer(door);
            var remote = new Remote(door);

            Console.WriteLine("Bruce starts Barking...");
            recognizer.Recognize(new Bark("rowlf"));
            Console.WriteLine("Bruce has gone outside....");

            try
            {
                Thread.Sleep(10000); ;
            }
            catch (Exception exception) { }

            Console.WriteLine("Bruce all done....");
            Console.WriteLine("...but he's stuck outside!");

            Bark smallDogBark = new Bark("yip");
            Console.WriteLine("A small dog starts barking..");
            recognizer.Recognize(smallDogBark);

            try
            {
                Thread.Sleep(5000);
            }
            catch (Exception exception) { }

            Console.WriteLine("Bruce Starts Barking....");
            recognizer.Recognize(new Bark("rowlf"));
            Console.WriteLine("Bruce's back inside....");
        }
    }
}
