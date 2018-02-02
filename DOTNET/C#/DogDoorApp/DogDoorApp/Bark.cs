using System;

namespace DogDoorApp
{
    public class Bark
    {
        private String sound;
        public Bark(String sound)
        {
            this.sound = sound;
        }

        public String Sound { get { return sound; } }

        public bool Equals(Object bark)
        {
            if (bark is Bark)
            {
                Bark otherBark = (Bark)bark;
                if (this.sound.Equals(otherBark.sound))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
