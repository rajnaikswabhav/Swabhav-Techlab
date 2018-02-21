using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;

namespace Techlabs.Euphoria.Kernel.Model
{
    [Table("STALL")]
    public class Stall : DimensionEntity
    {
        public int StallNo { get; set; }
        public int Price { get; set; }
        public bool IsBooked { get; set; }

        public virtual Section Section { get; set; }

        public virtual Pavilion Pavilion { get; set; }

        public virtual Exhibitor Exhibitor { get; set; }

        public Stall()
        {

        }

        public Stall(int stallNo)
        {
            Validate(stallNo);
            this.StallNo = stallNo;
        }
        private static void Validate(int stallNo)
        {

        }

        public static Stall Create(int stallNo)
        {
            return new Stall(stallNo);
        }

        public void Update(int stallNo)
        {
            Validate(stallNo);
            this.StallNo = stallNo;
        }

    }
}
