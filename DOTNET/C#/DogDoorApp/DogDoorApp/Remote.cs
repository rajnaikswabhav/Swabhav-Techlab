using System;

namespace DogDoorApp
{
    public class Remote
    {
        private DogDoor door;

        public Remote(DogDoor door)
        {
            this.door = door;
        }

        public void PressBotton()
        {
            Console.WriteLine("Pressing the Remote Control Button...");
            if (door.isOpen())
            {
                door.Close();
            }
            else
            {
                door.Open();
            }
        }
    }
}
