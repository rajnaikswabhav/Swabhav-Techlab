using System;


namespace InheritanceApp
{
    class Boy : Men
    {
        public Boy() {
            Console.WriteLine("Boy Class Created.......");
        }

        public void eat() {
            Console.WriteLine("This is Eat Method of Boy Class......");
        }

        public override void play() {
            Console.WriteLine("This is Play Method of Boy Class......");
        }
    }
}
