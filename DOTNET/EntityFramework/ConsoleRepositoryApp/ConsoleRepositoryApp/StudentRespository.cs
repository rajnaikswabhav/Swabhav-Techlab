using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRepositoryApp
{
    public class StudentRespository
    {
        private HiTechDbContext context;

        public StudentRespository(HiTechDbContext context)
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

        public IQueryable<Student> Find(Expression<Func<Student, bool>> exp)
        {
            IQueryable<Student> studentList = context.Students.Where(exp);
            return studentList;
        }
    }
}
