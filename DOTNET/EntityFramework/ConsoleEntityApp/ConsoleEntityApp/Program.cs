using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEntityApp
{
    class Program
    {
        static void Main(string[] args)
        {
            HiTechDbContext dbContext = new HiTechDbContext();
            //Case1(dbContext);
            //Case2(dbContext);
            //Case3(dbContext);
            //Case4(dbContext);
            //Case5(dbContext);
        }

        private static void Case5(HiTechDbContext dbContext)
        {
            var studentList = dbContext.Students.Where(s => s.Name.Contains("s"))
                                          .Where(s => s.Age > 21)
                                          .ToList()
                                          .Select(s => new
                                          {
                                              name = s.Name.Split(' ')[0],
                                              age = s.Age
                                          });
            foreach (var s in studentList)
            {
                Console.WriteLine("{0}, {1}", s.name, s.age);
            }
        }

        private static void Case4(HiTechDbContext dbContext)
        {
            var studentEnumarable = dbContext.Students.ToList()
                                                .Where(s => s.Name.Contains("s"))
                                                .Take(2);
            foreach (var s in studentEnumarable)
            {
                Console.WriteLine("{0}, {1}, {2}, {3}", s.Id, s.Name, s.Age, s.City);
            }
        }

        private static void Case3(HiTechDbContext dbContext)
        {
            var studentList = dbContext.Students.Where(s => s.Name.Contains("a")).Take(2)
                                                 .ToList();
            foreach (var s in studentList)
            {
                Console.WriteLine("{0}, {1}, {2}, {3}", s.Id, s.Name, s.Age, s.City);
            }
        }

        private static void Case2(HiTechDbContext dbContext)
        {
            var students = dbContext.Students.Select(m => new
            {
                name = m.Name,
                age = m.Age,
                city = m.City
            });

            foreach (var s in students)
            {
                Console.WriteLine("Name: {0}, Age: {1}, City: {2}", s.name, s.age, s.city);
            }
        }

        private static void Case1(HiTechDbContext dbContext)
        {
            Student student = new Student();
            student.Id = 101;
            student.Name = "Akash";
            student.Age = 21;
            student.City = "Ahmedabad";

            Student student2 = new Student();
            student2.Id = 102;
            student2.Name = "Parth";
            student2.Age = 22;
            student2.City = "Ahmedabad";

            Student student3 = new Student();
            student3.Id = 103;
            student3.Name = "Brijesh";
            student3.Age = 22;
            student3.City = "Mumbai";

            Student student4 = new Student();
            student4.Id = 104;
            student4.Name = "Devang";
            student4.Age = 22;
            student4.City = "Ahmedabad";

            dbContext.Students.Add(student);
            dbContext.Students.Add(student2);
            dbContext.Students.Add(student3);
            dbContext.Students.Add(student4);

            dbContext.SaveChanges();
        }
    }
}
