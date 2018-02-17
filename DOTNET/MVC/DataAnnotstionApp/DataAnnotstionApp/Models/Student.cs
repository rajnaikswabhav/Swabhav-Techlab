using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace DataAnnotstionApp.Models
{
    public class Student
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Required(ErrorMessage ="Name is Required..")]
        [StringLength(50,MinimumLength =4)]
        public string Name { get; set; }
        [DataAnnotstionApp.CustomAnnotation.MinAge(18)]
        public int Age { get; set; }
        [Required(ErrorMessage ="UserName is Required Field..")]
        public string UserName  { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}", ErrorMessage = "Incorrect Email Format")]
        public string Email { get; set; }
    }
}