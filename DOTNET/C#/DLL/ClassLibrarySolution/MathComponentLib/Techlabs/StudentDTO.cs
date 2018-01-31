using System;
using System.Collections.Generic;
using System.Text;

namespace MathComponentLib.Techlabs
{
    public class StudentDTO
    {
        private int _id = 1001;
        private static int nextId = 0;
        public StudentDTO()
        {
            _id = _id + nextId;
            nextId++;
        }
        public int Id
        {
            get
            {

                return _id;
            }
        }
        public String Name
        {
            get;
            set;
        }

        public String Location
        {
            get;
            set;
        }

        public override string ToString()
        {
            return "{Name : " + Name + "," + "Location : " + Location + " id : " + Id + "}";
        }
    }
}
