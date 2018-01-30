using System;
using System.Collections.Generic;
using System.Text;

namespace InheritanceApp
{
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
            AtThePark(new Men());
            AtThePark(new Boy());
            AtThePark(new Kid());
            AtThePark(new Infant());
        }

        private static void Case3()
        {
            Men men = new Boy();
            men.play();
            men.read();
        }

        private static void Case2()
        {
            Boy boy = new Boy();
            boy.eat();
            boy.play();
            boy.read();
        }

        private static void Case1()
        {
            Men men = new Men();
            men.read();
            men.play();
        }

        public static void AtThePark(Men men) {
            Console.WriteLine("At The Park:  ");
            men.play();
        }
    }
}
