using System;
using System.Collections.Generic;
using System.Text;

namespace InheritanceApp
{
    class Men
    {
        public Men() {
            Console.WriteLine("Men Class Created......");
        }

        public void read() {
            Console.WriteLine("This is read Method of Men Class....");
        }

        public virtual void play() {
            Console.WriteLine("This is play Method of Men class....");
        }
    }
}
