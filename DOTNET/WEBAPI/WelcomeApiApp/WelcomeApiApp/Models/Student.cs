using System;

namespace WelcomeApiApp
{
   
    public class Student
    {
        private  String _studentName;
        private  int _age;
        private  String _address;
        private  int? _id;

        public Student(String studentName, int age, String address,int? id)
        {
            _studentName = studentName;
            _age = age;
            _address = address;
            _id = id;
        }

        public Student() { }

        public String StudentName
        {
            get
            {
                return _studentName;
            }
            set {
                _studentName = value;
            }
        }

        public int Age
        {
            get
            {
                return _age;
            }
            set {
                _age = value;
            }
        }

        public String Address
        {
            get
            {
                return _address;
            }
            set {
                _address = value;
            }
        }

        public int? Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
    }
}
