using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartCore.Framework.Model
{
    public abstract class Entity
    {
        [Key]
        [Column(Order = 1)]
        public Guid Id { get; set; }
        public int Version { get; set; }
    }
}
