using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQWithCollection
{
    class Program
    {
        static void Main(string[] args)
        {
            
            List<Student> students = new List<Student>
            {
                 new Student {StudentID=101,FirstName="Akash",LastName="Malaviya",CGPA=6.97f,Location="Ahmedabad",Age=21},
                 new Student {StudentID=102,FirstName="Brijesh",LastName="Jadhav",CGPA=7.97f,Location="Mumbai",Age=22},
                 new Student {StudentID=103,FirstName="Parth",LastName="Jodhani",CGPA=6.50f,Location="Ahmedabad",Age=22},
                 new Student {StudentID=104,FirstName="Mahavir",LastName="Kasela",CGPA=6.70f,Location="Ahmedabad",Age=21},
                 new Student {StudentID=105,FirstName="Shreyash",LastName="Rayani",CGPA=8.97f,Location="Ahmedabad",Age=21},
            };

            var sortedByNameList = students.OrderByDescending(n => n.FirstName);

            foreach(var student in sortedByNameList)
            {
                Console.WriteLine("id:{0},FirstName: {1},LastName: {2},CGPA: {3},Location: {4},Age: {5}",
                    student.StudentID,student.FirstName,student.LastName,student.CGPA,student.Location,student.Age);
            }
            Console.WriteLine();
            var firstNameLastName = students.Select((s) => new  {
                  F = s.FirstName,
                  L= s.LastName
            });

            foreach(var name in firstNameLastName)
            {
                Console.WriteLine("FirstName: {0} , LastName: {1}",name.F,name.L);
            }

            var topTwoStudentList = students.OrderByDescending(n => n.CGPA)
                                    .Take(2);

            foreach(var student in topTwoStudentList)
            {
                Console.WriteLine("id:{0},FirstName: {1},LastName: {2},CGPA: {3},Location: {4},Age: {5}",
                    student.StudentID, student.FirstName, student.LastName, student.CGPA, student.Location, student.Age);
            }
            Console.WriteLine("Enter Student Id: ");
            int id = int.Parse(Console.ReadLine());

            var displayBySingleId = students.Where(n => n.StudentID == id).Select(n => n).Single();
           
            Console.WriteLine("id:{0},FirstName: {1},LastName: {2},CGPA: {3},Location: {4},Age: {5}",
                 displayBySingleId.StudentID, displayBySingleId.FirstName, displayBySingleId.LastName, displayBySingleId.CGPA, displayBySingleId.Location, displayBySingleId.Age);

            var averageAge = students.Select(n => n.Age).Average();

            Console.WriteLine(averageAge);
        }
    }
}
