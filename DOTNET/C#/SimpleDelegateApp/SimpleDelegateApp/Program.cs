using System;

namespace SimpleDelegateApp
{
    delegate void DSaySomething(String name);
    class Program
    {
        static void Main(string[] args)
        {
            //Case1();
            //Case2();
            //Case3();
            Case4();
        }

        private static void Case4()
        {
            MessageWizard(delegate (String name)
            {
                Console.WriteLine("Inside Anonymous Function....");
                Console.WriteLine("{0} says Hello.....", name);
            });

            MessageWizard((n) => Console.WriteLine("Inside Lemda function: {0} says Hello:", n));
        }

        private static void Case3()
        {
            MessageWizard(SayHello);
            MessageWizard(SayGoodBye);
        }

        private static void Case2()
        {
            DSaySomething obj;
            obj = SayHello;
            obj += SayGoodBye;
            obj("champ");
        }

        private static void Case1()
        {
            DSaySomething obj = SayHello;
            obj("Akash");
            obj = SayGoodBye;
            obj("Brijesh");
            // obj = Foo;
            //obj();
        }

        private static void SayHello(String name)
        {
            Console.WriteLine("{0} says Hello.....", name);
        }

        private static void SayGoodBye(String name)
        {
            Console.WriteLine("{0} says GoodBye.....", name);
        }

        private static void Foo()
        {
            Console.WriteLine("Inside Foo.....");
        }

        private static void MessageWizard(DSaySomething obj)
        {
            Console.WriteLine("Inside Message Wizard.....");
            obj("MessageWizard");
        }
    }
}
