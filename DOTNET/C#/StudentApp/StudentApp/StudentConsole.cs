using System;
using System.Collections.Generic;
using System.Text;

namespace StudentApp
{
    class StudentConsole
    {
        private const int ADD_CHOICE = 1;
        private const int DISPLAY_CHOICE = 2;
        private const int SEARCH_STUDENT_DETAILS = 3;
        private const int DELETE_STUDENT_DETAILS = 4;


        private List<Student> listOfStudents;
        private StudentService studentService = new StudentService();

        public void Start()
        {
            Console.WriteLine("Press 1 To Add Students");
            Console.WriteLine("Press 2 To Display Students");
            Console.WriteLine("Press 3 To Search Student detail");
            Console.WriteLine("Press 4 To Delete Student");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case ADD_CHOICE :
                    GetDetails();
                    break;

                case DISPLAY_CHOICE :
                    PrintData();
                    break;

                case SEARCH_STUDENT_DETAILS:
                    GetSearch();
                    break;

                case DELETE_STUDENT_DETAILS:
                    GetDelete();
                    break;
            }
        }
        public void GetDetails()
        {
            Console.Write("Enter Student Name : ");
            String name = Console.ReadLine();

            Console.Write("Enter Student Age: ");
            int age = int.Parse(Console.ReadLine());

            Console.Write("Enter Student Location : ");
            String address = Console.ReadLine();

            var student = new Student(name, age, address);
            studentService.AddStudents(student);

            Console.WriteLine("Your Data Saved successfully....");
            Console.WriteLine();
            Start();
        }

        public void PrintData()
        {
            listOfStudents = studentService.GetStudents;
            foreach (Student stu in listOfStudents)
            {
                Console.WriteLine("Student Name: " + stu.StudentName);
                Console.WriteLine("Student Age: " + stu.Age);
                Console.WriteLine("Student Location: " + stu.Address);
                Console.WriteLine();
            }
        }

        public void GetSearch()
        {
            Console.WriteLine("Enter Student Name: ");
            String studentName = Console.ReadLine();
            Student searchedStudent = studentService.Search(studentName);

            Console.WriteLine("Student Id: "+searchedStudent.Id);
            Console.WriteLine("Student Name: "+searchedStudent.StudentName);
            Console.WriteLine("Student Age: "+searchedStudent.Age);
            Console.WriteLine("Student Location: "+searchedStudent.Address);
        }

        public void GetDelete() {
            Console.WriteLine("Enter student id to delete: ");
            Console.WriteLine(studentService.DeleteStudentData(Console.ReadLine()));
        }

    }
}
