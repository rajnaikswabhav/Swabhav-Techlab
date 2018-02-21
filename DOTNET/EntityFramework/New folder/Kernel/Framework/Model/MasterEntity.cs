using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techlabs.Euphoria.Kernel.Framework.Model
{
    public class MasterEntity : AggregateEntity
    {
        private static TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        public DateTime CreatedOn {get; private set;}
        public Guid? CreatedBy { get; set; }
        public DateTime LastModifiedOn { get; private set; }
        public Guid? LastModifiedBy { get; set; }
        public bool IsDeleted { get; private set; }

        public MasterEntity()
        {
            CreatedOn = LastModifiedOn = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            //TODO: Set CreatedBy and ModifiedBy

            //CreatedBy = "";
            //LastModifiedBy = "test_user";

            //TODO: Need to check ??
            this.Id = Guid.NewGuid();
        }

        public void MarkAsDeleted()
        {
            IsDeleted = true;
            LastModifiedOn = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
            //TODO: Dont forget to set the Modified By     
        }
    }
}
