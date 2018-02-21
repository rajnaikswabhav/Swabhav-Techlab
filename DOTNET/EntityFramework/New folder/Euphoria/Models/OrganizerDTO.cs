using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Techlabs.Euphoria.Kernel.Framework.Model;

namespace Techlabs.Euphoria.API.Models
{
    public class OrganizerDTO
    {
        public Guid Id { get; set; }
        [Required]
        public int TenantId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}