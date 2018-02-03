using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsetApp
{
    class Student
    {
        private readonly int id;
        private readonly int standard;
        private readonly string studentName;

        public Student(int id, int standard, string studentName)
        {
            this.id = id;
            this.standard = standard;
            this.studentName = studentName;
        }

        public int Id {get { return id; } }
        public int Standard {get { return standard; } }
        public string StudentName {get { return studentName; } }

        public override bool Equals(object obj)
        {
            if(this == obj) { return true; }
            if(obj == null) { return false; }
            if(GetType() != obj.GetType()) { return false; }
            Student other = (Student) obj;
            if(Id != other.Id) { return false; }
            if(Standard != other.Standard) { return false; }
            return true;
        }

        public override int GetHashCode()
        {
            int prime = 31;
            int result = 1;
            result = prime * result + Id;
            result = prime * result + Standard;
            return result;

        }
    }
}
