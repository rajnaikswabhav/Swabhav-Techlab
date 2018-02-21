using ConsoleRepositoryApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DIApiApp
{
    public class StudentEFRepository : IRepository<Student>
    {
        private HiTechDbContext context;
        public StudentEFRepository(HiTechDbContext context)
        {
            this.context = context;
        }
        public void Add(Student student)
        {
            context.Students.Add(student);
            context.SaveChanges();
        }

        public IQueryable<Student> Get()
        {
            return context.Students;
        }
    }
}