using System;
using System.Collections.Generic;
using System.Text;

namespace InterfacePolymorphismApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var men = new Men();
            var boy = new Boy();

            AtTheParty(men);
            AtTheParty(boy);
            AtTheMovie(boy);
            //AtTheMovie(men);
        }

        public static void AtTheMovie(IEMotionable obj)
        {
            Console.WriteLine("In the Movie....");
            obj.Cry();
            obj.Laugh();
        }

        public static void AtTheParty(IMAnneravle obj)
        {
            Console.WriteLine("In the Party.....");
            obj.Wish();
            obj.Depart();
        }
    }
}
