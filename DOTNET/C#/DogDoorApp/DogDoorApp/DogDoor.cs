using System;
using System.Collections.Generic;

namespace DogDoorApp
{
    public class DogDoor
    {
        private bool open;
        private List<Bark> allowedBark;

        public DogDoor()
        {
            this.allowedBark = new List<Bark>();
            open = false;
        }

        public void AddAllowedBark(Bark bark)
        {
            allowedBark.Add(bark);
        }

        public List<Bark> AllowedBark { get { return allowedBark; } }

        public void Open()
        {
            Console.WriteLine("The Dog door opens..");

            open = true;
            Close();
        }

        public void Close()
        {
            Console.WriteLine("The DogDoor closes...");
            open = false;
        }

        public bool isOpen()
        {
            return open;
        }
    }
}
