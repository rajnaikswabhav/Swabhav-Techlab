using Modules.EventManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Model;

namespace Modules.LayoutManagement
{
    [Table("LAYOUTPLAN")]
    public class LayoutPlan : MasterEntity
    {
        public string VersionNo { get; set; }
        public bool isLocked { get; set; }
        public virtual Event Event { get; set; }       
        public virtual LayoutPlan PreviousLayoutPlan { get; set; }

        public LayoutPlan()
        {
            
        }

        public LayoutPlan(string versionNo, bool isLocked) : this()
        {
            Validate(versionNo, isLocked);

            this.VersionNo = versionNo;
            this.isLocked = isLocked;
        }

        private static void Validate(string versionNo, bool isLocked)
        {
            if (string.IsNullOrEmpty(versionNo))
                throw new ValidationException("Invalid Version No");
        }

        public static LayoutPlan Create(string versionNo, bool isLocked)
        {
            return new LayoutPlan(versionNo, isLocked);
        }

        public void Update(string versionNo, bool isLocked)
        {
            Validate(versionNo, isLocked);

            this.VersionNo = versionNo;
            this.isLocked = isLocked;
        }
    }
}
