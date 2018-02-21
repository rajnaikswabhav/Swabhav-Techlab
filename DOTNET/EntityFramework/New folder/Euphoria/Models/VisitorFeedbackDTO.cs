using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class VisitorFeedbackDTO
    {
        public string VisitorEmailId { get; set; }
        public string PhoneNumber { get; set; }
        public int Pincode { get; set; }
        [Required]
        public int SpendRange { get; set; }
        [Required]
        public int EventRating { get; set; }
        [Required]
        public bool RecommendToOther { get; set; }
        [Required]
        public int ReasonForVisiting { get; set; }
        [Required]
        public int KnowAboutUs { get; set; }
        public string Comment { get; set; }

        public List<CategoryDTO> Categories { get; set; }
        public List<CountryDTO> Countries { get; set; }
        public List<ExhibitorTypeDTO> ExhibitorTypes { get; set; }

        public VisitorFeedbackDTO()
        {
            Categories = new List<CategoryDTO>();
            Countries = new List<CountryDTO>();
            ExhibitorTypes = new List<ExhibitorTypeDTO>();
        }
    }
}