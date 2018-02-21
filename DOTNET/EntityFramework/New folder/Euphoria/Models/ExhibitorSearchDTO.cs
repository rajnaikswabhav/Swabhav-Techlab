using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class ExhibitorSearchDTO
    {
        [Required]
        public string Name { get; set; }
        public List<StallDTO> Stalls { get; set; }
        // public  List<ExhibitionDTO> Exhibitions { get; set; }
        public List<CategoryDTO> Categories { get; set; }
        public CountryDTO Country { get; set; }
        public StateDTO State { get; set; }

        public ExhibitorSearchDTO()
        {
            Stalls = new List<StallDTO>();
            // Exhibitions = new List<ExhibitionDTO>();
            Categories = new List<CategoryDTO>();
        }
    }
}