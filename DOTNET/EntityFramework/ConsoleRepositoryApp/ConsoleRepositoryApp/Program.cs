using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRepositoryApp
{
    class Program
    {
        static void Main(string[] args)
        {
            StudentRespository repo = new StudentRespository(new HiTechDbContext());
            //AddStudents(repo);
            //GetStudents(repo);

            var studentList = repo.Find(s => s.CGPA > 7);
            var list = studentList.Take(3);
            var finalStudentList = list.Select(s => new
            {
                name = s.Name,
                age = s.Age
            });

            foreach(var s in finalStudentList)
            {
                Console.WriteLine("{0}, {1}",s.name,s.age);
            }
        }

        private static void GetStudents(StudentRespository repo)
        {
            var studentList = repo.Get()
                                               .Where(s => s.Name.Length > 4)
                                               .ToList();

            foreach (var s in studentList)
            {
                Console.WriteLine("{0}, {1}, {2}, {3}, {4}", s.Id, s.Name, s.Age, s.City, s.CGPA);
            }
        }

        private static void AddStudents(StudentRespository repo)
        {
            repo.Add(new Student { Id = 101, Name = "Akash", Age = 21, City = "Ahmedabad", CGPA = 6.97 });
            repo.Add(new Student { Id = 102, Name = "Gaurang", Age = 20, City = "Ahmedabad", CGPA = 7.50 });
            repo.Add(new Student { Id = 103, Name = "Brijesh", Age = 22, City = "Mumbai", CGPA = 8.10 });
            repo.Add(new Student { Id = 104, Name = "Himanshu", Age = 21, City = "Mumbai", CGPA = 6.40 });
            repo.Add(new Student { Id = 105, Name = "Jay", Age = 22, City = "Mumbai", CGPA = 7.93 });
        }
    }
}
