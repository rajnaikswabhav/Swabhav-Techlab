using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace StudentApp
{
    class StudentService
    {
        private List<Student> _studentList = new List<Student>();

        public StudentService()
        {
            IntializeList();
        }

        private void IntializeList()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStreamIn = new FileStream("student.binary", FileMode.Open, FileAccess.Read);

            try
            {
                using (fileStreamIn)
                {
                    _studentList = (List<Student>)binaryFormatter.Deserialize(fileStreamIn);
                }

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void AddStudents(Student student)
        {
            _studentList.Add(student);
            Save();
        }


        public void Save()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream("student.binary", FileMode.Create, FileAccess.Write);
            try
            {
                using (fileStream)
                {
                    binaryFormatter.Serialize(fileStream, _studentList);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<Student> Students
        {
            get
            {
                return _studentList;
            }
        }

        public Student Search(String studentName)
        {
            foreach (Student student in _studentList)
            {
                if (studentName.Equals(student.StudentName))
                {
                    return student;
                }
            }
            return null;
        }

        public String DeleteStudentData(String id)
        {
            foreach (Student student in _studentList)
            {
                if (id.Equals(student.Id))
                {
                    _studentList.Remove(student);
                    Save();
                    return "Student deleted by Id : " + student.Id;
                }
            }
            return "No data Found with this Id.";
        }
    }
}