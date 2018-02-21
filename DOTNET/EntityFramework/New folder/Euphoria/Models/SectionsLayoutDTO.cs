using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public class SectionsLayoutDTO
    {
        public string Version_No { get; set; }
        public List<SectionsDTO> Sections { get; set; }
        public SectionsLayoutDTO()
        {
            Sections = new List<SectionsDTO>();

        }

    }
    public class SectionsDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int X_Coordinate { get; set; }
        public int Y_Coordinate { get; set; }
        public Guid ExhibitionId { get; set; }
        public Guid SectionTypeId { get; set; }
        public List<SectionsDTO> SectionsList { get; set; }
        public List<AccesssDTO> AccessList { get; set; }
        public SectionsDTO()
        {
            SectionsList = new List<SectionsDTO>();
            AccessList = new List<AccesssDTO>();
        }
    }
    public class AccesssDTO
    {
        public bool IsEntry { get; set; }
        public bool IsExit { get; set; }
        public bool IsEmergencyExit { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int X_Coordinate { get; set; }
        public int Y_Coordinate { get; set; }
    }
    
}