using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace StudentApp
{
    class StudentConsole
    {
        private List<Student> studentList = new List<Student>();

        public StudentConsole()
        {
            //InIt();
        }

        public void InIt()
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStreamIn = new FileStream("student.binary", FileMode.Open, FileAccess.Read);

            try
            {
                using (fileStreamIn)
                {
                    List<Student> listStudent =(List<Student>)binaryFormatter.Deserialize(fileStreamIn);
                    studentList = listStudent;
                }


            }
            catch (Exception e)
            {
                Console.WriteLine("Exception :" + e);
            }
        }

        public void AddStudent()
        {
            Console.Write("Enter Student Name : ");
            String name = Console.ReadLine();

            Console.Write("Enter Student Age: ");
            int age = int.Parse(Console.ReadLine());

            Console.Write("Enter Student Location : ");
            String address = Console.ReadLine();

            var student = new Student(name, age, address);
            studentList.Add(student);
            Save(studentList);
        }

        public void Save(List<Student> listOfStudents)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStream = new FileStream("student.binary",FileMode.Append,FileAccess.Write);
            try
            {
                    using (fileStream)
                    {
                        binaryFormatter.Serialize(fileStream, listOfStudents);
                        Console.WriteLine("Serialized......");
                    }
                
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception : " + e);
            }
            Console.WriteLine("File Created....");
            Console.WriteLine(studentList.Count);
        }

        public void GetDetails()
        {

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fileStreamIn = new FileStream("student.binary", FileMode.Open, FileAccess.Read);

            try
            {
                using (fileStreamIn)
                {
                    Student student = (Student)binaryFormatter.Deserialize(fileStreamIn);
                    studentList.Add(student);
                }

                fileStreamIn.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception :" + e);
            }
        }

        public void showData()
        {
            foreach (Student stu in studentList)
            {
                Console.WriteLine("Student Name: " + stu.StudentName);
                Console.WriteLine("Student Age: " + stu.Age);
                Console.WriteLine("Student Location: " + stu.Address);
            }
        }
    }
}
