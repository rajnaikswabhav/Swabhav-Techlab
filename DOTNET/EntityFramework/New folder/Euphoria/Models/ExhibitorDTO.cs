using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Techlabs.Euphoria.Kernel.Model;

namespace Techlabs.Euphoria.API.Models
{
    public class ExhibitorDTO
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string EmailId { get; set; }
        [Required]
        public string PhoneNo { get; set; }
        [Required]
        public string CompanyName { get; set; }
        public string RegistrationType { get; set; }
        public string Comment { get; set; }
        public string Designation { get; set; }
        public int Age { get; set; }
        public string CompanyDescription { get; set; }
        public string Address { get; set; }
        public int PinCode { get; set; }
        public string Password { get; set; }
        public string EnquiryDate { get; set; }

        public Guid ExhibitorStatusId { get; set; }

        public ExhibitorIndustryTypeDTO ExhibitorIndustryType { get; set; }
        public ExhibitorTypeDTO ExhibitorType { get; set; }
        public ExhibitorStatusDTO ExhibitorStatusDTO { get; set; }
        public List<StallDTO> Stalls { get; set; }
        // public  List<ExhibitionDTO> Exhibitions { get; set; }
        public List<CategoryDTO> Categories { get; set; }
        public CountryDTO Country { get; set; }
        public StateDTO State { get; set; }

        public ExhibitorDTO()
        {
            Stalls = new List<StallDTO>();
            // Exhibitions = new List<ExhibitionDTO>();
            Categories = new List<CategoryDTO>();
        }
    }
}