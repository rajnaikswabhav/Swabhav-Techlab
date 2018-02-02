using System;
using System.Collections.Generic;
using System.Text;

namespace GuitarApp
{
    public class Guitar
    {
        private readonly String serialNumber;
        private readonly double price;
        private GuitarSpec spec;

        public Guitar(String serialNumber, double price, GuitarSpec spec)
        {
            this.serialNumber = serialNumber;
            this.price = price;
            this.spec = spec;
        }

        public String SerialNumber { get { return serialNumber; } }
        public double Price { get { return price; } }
        public GuitarSpec Spec { get { return spec; } }
    }
}
