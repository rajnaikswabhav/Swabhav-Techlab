using ConsoleRepositoryApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DIApiApp
{
    public interface IRepository<T> where T:Student
    {
        void Add(Student student);
        IQueryable<T> Get();
    }
}
