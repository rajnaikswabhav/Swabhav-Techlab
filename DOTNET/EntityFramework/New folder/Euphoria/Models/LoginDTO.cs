using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class LoginDTO
    {
        public Guid Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public string EmailId { get; set; }
        public string Color { get; set; }
        public Guid PartnerId { get; set; }
    }
}