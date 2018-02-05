using System;

namespace StudentCore
{
    [Serializable]
    public class Student
    {
        private readonly String _studentName;
        private readonly int _age;
        private readonly String _address;
        private readonly String _id;
        public Student(String studentName, int age, String address)
        {
            _studentName = studentName;
            _age = age;
            _address = address;
            _id = Guid.NewGuid().ToString("N");
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

        public String Address
        {
            get
            {
                return _address;
            }
        }

        public String Id
        {
            get
            {
                return _id;
            }
        }
    }
}
