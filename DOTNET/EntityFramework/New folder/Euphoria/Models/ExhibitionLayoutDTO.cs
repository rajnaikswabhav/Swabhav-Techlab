using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Techlabs.Euphoria.Kernel.Model;

namespace Techlabs.Euphoria.API.Models
{
    public class ExhibitionLayoutDTO
    {
        public string Version_No { get; set; }
        public List<SectionDTO> ExhibitionSection { get; set; }        
    }
    public class SectionDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int X_Coordinate { get; set; }
        public int Y_Coordinate { get; set; }
        public List<SectionDTO> SectionsList { get; set; }
        public List<StallsDTO> StallList { get; set; }
        public List<AccessDTO> AccessList { get; set; }
    }
    public class StallsDTO
    {
        public Guid Id { get; set; }
        public int StallNo { get; set; }
        public bool IsBooked { get; set; }
        public double Price { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int X_Coordinate { get; set; }
        public int Y_Coordinate { get; set; }
        public bool IsRequested { get; set; }
        public Guid PartnerId { get; set; }
        public string PartnerColor { get; set; }
    }
    public class AccessDTO
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