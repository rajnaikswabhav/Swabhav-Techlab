using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class VisitorDTO
    {
        public VisitorDTO()
        {
        }

        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        
        public int Pincode { get; set; }
        [Required]
        public string MobileNo { get; set; }
        [Required]
        public string EmailId { get; set; }
        public string DateOfBirth { get; set; }
        public bool Gender { get; set; }
        public string Education { get; set; }
        public string Income { get; set; }
        public bool? IsMobileVerified { get; set; }
    }
}