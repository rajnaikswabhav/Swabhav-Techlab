using ConsoleRepositoryApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace DIApiApp.Controllers
{
    [RoutePrefix("api/SwabhavTech/v1/Student")]
    public class StudentController : ApiController
    {
        private IRepository<Student> repo;

        public StudentController(IRepository<Student> repo) {
            this.repo = repo;
        }

        public IHttpActionResult GetAllStudent()
        {
            var studentList = repo.Get().ToList();
            return Ok(studentList);
        }

        public IHttpActionResult Post(Student student)
        {
            repo.Add(student);
            return Ok();
        }
    }
}