using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Techlabs.Euphoria.API.Controllers
{

    public class StudentDTO {

        public int RollNo { get; set; }
        public string Name { get; set; }
        public int Age { get;set;}
        public string Email { get; set; }
        public string Date { get; set; }
        public bool isMale { get; set; }
    }
    [RoutePrefix("api/v1/techlabs/test/students")]
    public class TestStudentController : ApiController
    {
       static List<StudentDTO> _studentDTOList;

         static TestStudentController() {

            _studentDTOList = new List<StudentDTO>();

            _studentDTOList.Add(new StudentDTO{
                 RollNo= 101,
                 Age=22,
                 Email="abc@abc.com",
                 Date="20/12/2012",
                 isMale=true,
                 Name="Swapnil"
            });

            _studentDTOList.Add(new StudentDTO
            {
                RollNo = 102,
                Age = 23,
                Email = "abc@abc.com",
                Date = "20/12/2012",
                isMale = true,
                Name = "Nikhil"
            });
            _studentDTOList.Add(new StudentDTO
            {
                RollNo = 103,
                Age = 23,
                Email = "abc@abc.com",
                Date = "20/12/2012",
                isMale = false,
                Name = "Ketki"
            });
        }

        
        [Route("")]
        public IHttpActionResult Get()
        {
            return  Ok(_studentDTOList);
        }

        [Route("{id}")]
        public IHttpActionResult Get(int id)
        {
            return Ok( _studentDTOList.Find(x=>x.RollNo== id));
        }

        [Route("")]
        public IHttpActionResult Post(StudentDTO student)
        {
            if (student == null)
                return NotFound();

            _studentDTOList.Add(student);
            return Ok();
        }

        [Route("{id}")]
        public IHttpActionResult Put(int id, StudentDTO student)
        {
            if (student == null)
                return NotFound();

            _studentDTOList.Remove(_studentDTOList.Find(x => x.RollNo == id));
            _studentDTOList.Add(student);

            return Ok();
        }

        [Route("{id}")]
        public IHttpActionResult Delete(int id)
        {          
            _studentDTOList.Remove(_studentDTOList.Find(x => x.RollNo == id));
            return Ok();
        }

        [Route("citrusview")]
        public HttpResponseMessage GetCitrusView()
        {
            var response = new HttpResponseMessage();

            string htmlPage= "<!DOCTYPE html><html><head><style>header,a{background-color:#fdbc2f; color:white; text-align:center; padding:5px;text-decoration: none;}html, body{position: absolute; width: 100%; min-height: 100%; padding: 0; margin: 0;}body:after{line-height: 100px; white-space: pre;}footer{position: absolute; width: 100%; height: 50px; bottom: 0px; margin-top: -100px; text-align: center;}nav{background-color: yellow;}article{background-color: purple; color: white;}footer{background-color: #fdbc2f;}</style></head><body><header><h2><a href='index.html'>GS Marketing</a></h2></header><section></section><div style='text-align: center;margin-top:3em'>Hi</div><footer >Copyright &copy; GS Marketing Associates </footer></body></html>";
            response.Content = new StringContent(htmlPage);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;         
        }
    }
}
