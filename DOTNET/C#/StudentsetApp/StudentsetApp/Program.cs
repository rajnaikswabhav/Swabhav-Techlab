using System;
using System.Collections.Generic;

namespace StudentsetApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var student = new Student(101, 5, "Akash");
            HashSet<Student> studentSet = new HashSet<Student>();
            studentSet.Add(student);

            var student2 = new Student(101, 5, "Akash");
            studentSet.Add(student2);

            Print(studentSet);
        }

        public static void Print(HashSet<Student> studentSet)
        {
            foreach (var student in studentSet)
            {
                Console.WriteLine("Student Id:{0}, Standard:{1}, Student Name: {2}",student.Id,student.Standard,student.StudentName);
            }
        }
    }
}
