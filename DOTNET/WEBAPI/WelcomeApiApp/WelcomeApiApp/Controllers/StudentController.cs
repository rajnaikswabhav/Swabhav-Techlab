
using WelcomApiApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WelcomeApiApp.Controllers
{
    [RoutePrefix("api/v1/SwabhavTechlab/Student")]
    public class StudentController : ApiController

    {
        private readonly StudentService _service = new StudentService();

        [Route("GetAllStudents")]
        public IHttpActionResult Get()
        {
            var allStudent = _service.Students;
            return Ok(allStudent);
        }

        [Route("GetAllStudent/{studentId}")]
        public IHttpActionResult GetAllStudent(int? studentId)
        {
            if (studentId == null) { return NotFound(); }
            else
            {
                var student = _service.GetStudentByID(studentId);
                if (student == null) { return NotFound(); }
                else { return Ok(student); }
            }
        }

        [Route("AddStudent")]
        public IHttpActionResult PostAddStudent(Student student)
        {
           
            if (student == null) {
                return InternalServerError();
            }
            else {
                _service.AddStudent(student);
                return Ok("Data Added.");
            }
            
        }

        [Route("UpdateStudent/{studentId}")]
        public IHttpActionResult PutUpdateStudent(int? studentId)
        {
         
            if (studentId == null)
            {
                return InternalServerError();
            }
            else
            {
                var student = _service.GetStudentByID(studentId);
                _service.UpdateStudent(student);
                return Ok("Data Added.");
            }
        }
    }
}