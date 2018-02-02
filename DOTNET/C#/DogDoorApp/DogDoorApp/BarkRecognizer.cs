using System;
using System.Collections.Generic;

namespace DogDoorApp
{
    public class BarkRecognizer
    {
        private DogDoor door;

        public BarkRecognizer(DogDoor door)
        {
            this.door = door;
        }

        public void Recognize(Bark bark)
        {
            Console.WriteLine("   BarkRecognizer: Heard a '" + bark.Sound
                + "'");
            List<Bark> allowedBarks = door.AllowedBark;
            foreach (Bark b in allowedBarks)
            {
                Bark allowedBark = b;
                if (allowedBark.Equals(bark))
                {
                    door.Open();
                    return;
                }
            }
            Console.WriteLine("This Dog is not Allowed..");
        }
    }
}
