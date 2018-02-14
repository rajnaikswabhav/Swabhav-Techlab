using System;
using System.Linq;
using System.Collections.Generic;

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
            listOfStudent = loadBinaryData.Students;
        }

        public void AddStudent(List<Student> studentList)
        {
            Console.WriteLine("Inside Add student....");
            dataSaver.Save(studentList);
        }

        public List<Student> GetDetails()
        {
            return listOfStudent;
        }

        public List<Student> Search(System.Func<Student, bool> predicate)
        {
            return listOfStudent.Where(predicate).ToList();
        }

    }
}
