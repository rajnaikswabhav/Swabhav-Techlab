using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentCore
{
    public class StudentService
    {
        private BinaryDataSaver dataSaver;
        private BinaryDataLoader loadBinaryData;
        private List<Student> listOfStudent;

        public StudentService()
        {
            dataSaver = new BinaryDataSaver();
            loadBinaryData = new BinaryDataLoader();
            listOfStudent = new List<Student>();
        }

        public void AddStudent(List<Student> studentList)
        {
            Console.WriteLine("Inside Add student....");
            dataSaver.Save(studentList);
        }

        public List<Student> GetDetails()
        {
           return loadBinaryData.Students;
        }
    }
}
