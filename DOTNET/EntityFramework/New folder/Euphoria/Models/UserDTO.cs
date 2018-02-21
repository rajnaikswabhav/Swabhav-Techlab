using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class UserDTO
    {
        public UserDTO()
        {
        }

        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}