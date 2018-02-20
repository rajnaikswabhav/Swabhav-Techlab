using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using WelcomeApiApp;

namespace WelcomApiApp
{
    class StudentService
    {
        private static List<Student> _studentList = new List<Student>();

        static StudentService()
        {
            IntializeList();
        }

        private static void IntializeList()
        {
            Student student = new Student("Akash", 21, "Ahmedabad",101);
            Student student2 = new Student("Brijesh", 22, "Mumbai",102);
            Student student3 = new Student("Parth", 22, "Ahmedabad",103);
            _studentList.Add(student);
            _studentList.Add(student2);
            _studentList.Add(student3);
        }

        public List<Student> Students
        {
            get
            {
                return _studentList;
            }
        }

        public void AddStudent(Student student)
        {
            _studentList.Add(student);
        }

        public Student GetStudentByID(int? id)
        {
            foreach (var s in _studentList)
            {
                if(s.Id == id)
                {
                    return s;
                }
            }
            return null;
        }

        public void UpdateStudent(Student student)
        {
            foreach(var s in _studentList)
            {
                if(s.Id == student.Id)
                {
                    _studentList.Remove(s);
                    _studentList.Add(student);
                }
            }
        }
    }
}