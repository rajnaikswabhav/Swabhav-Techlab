using System;
using System.Collections.Generic;


namespace GuitarApp
{
    public class Inventory
    {
        private List<Guitar> guitars;

        public Inventory()
        {
            guitars = new List<Guitar>();
        }

        public void AddGuitar(String serialNumber, double price, GuitarSpec spec)
        {
            Guitar guitar = new Guitar(serialNumber, price, spec);
            guitars.Add(guitar);
        }

        public Guitar GetGuitar(String serialNumber)
        {
            foreach (Guitar guitar in guitars)
            {
                if (guitar.SerialNumber.Equals(serialNumber))
                {
                    return guitar;
                }
            }
            return null;
        }

        public List<Guitar> Search(GuitarSpec searchSepc)
        {
            List<Guitar> matchingGuitar = new List<Guitar>();
            foreach (var guitar in guitars)
            {
                if (guitar.Spec.Matches(searchSepc))
                {
                    matchingGuitar.Add(guitar);
                }
            }
            return matchingGuitar;
        }
    }
}
