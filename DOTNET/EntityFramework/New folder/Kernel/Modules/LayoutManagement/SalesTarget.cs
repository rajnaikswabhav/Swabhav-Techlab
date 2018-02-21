using Modules.EventManagement;
using Modules.SecurityManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;
using Techlabs.Euphoria.Kernel.Model;

namespace Techlabs.Euphoria.Kernel.Modules.LayoutManagement
{
    [Table("SALESTARGET")]
    public class SalesTarget : MasterEntity
    {
        public int Target { get; set; }
        public int TargetAchieved { get; set; }

        public virtual ExhibitorIndustryType ExhibitorIndustryType { get; set; }
        public virtual ExhibitorType ExhibitorType { get; set; }
        public virtual Login Login { get; set; }
        public virtual Event Event { get; set; }
        public virtual State State { get; set; }
        public virtual Country Country { get; set; }


        public SalesTarget()
        {
        }

        public SalesTarget(int target) : this()
        {
            Validate(target);
            this.Target = target;
        }

        private static void Validate(int target)
        {        
        }

        public static SalesTarget Create(int target)
        {
            return new SalesTarget(target);
        }

        public void Update(int target)
        {
            Validate(target);
            this.Target = target;
        }
    }
}
