using System;
using System.Collections.Generic;
using System.Text;

namespace CollageApp
{
    public class Person
    {
        private readonly int id;
        private readonly String address;
        private readonly String dob;

        public Person(int id, String address, String dob)
        {
            this.id = id;
            this.address = address;
            this.dob = dob;
        }

        public int Id { get { return id; } }
        public String Address { get { return address; } }
        public String Dob { get { return dob; } }
    }
}
