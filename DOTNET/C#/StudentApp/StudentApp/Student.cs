using System;

namespace StudentApp
{
    [Serializable]
    class Student
    {
        private readonly String _studentName;
        private readonly int _age;
        private readonly String _address;

        public Student(String studentName, int age, String address) {
            _studentName = studentName;
            _age = age;
            _address = address;
        }

        public String StudentName
        {
            get
            {
                return _studentName;
            }
        }

        public int Age
        {
            get
            {
                return _age;
            }
        }

        public String Address {
            get
            {
                return _address;
            }
        }
    }
}
