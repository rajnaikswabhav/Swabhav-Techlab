using System;

namespace Techlabs.Euphoria.API.Models
{
    public class ExhibitorLocationDTO
    {
        public Guid PavilionId { get; set; }
        public Guid StallId { get; set; }
    }

    public class ExhibitorAllocationDTO
    {
        public Guid ExhibitionId { get; set; }
        public ExhibitorLocationDTO[] Locations { get; set; }
    }
}