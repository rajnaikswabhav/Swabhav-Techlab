using Modules.SecurityManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techlabs.Euphoria.Kernel.Modules.SecurityManagement
{
    [Table("LOGINSESSION")]
    public class LoginSession : Framework.Model.MasterEntity
    {
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public virtual Login Login { get; set; }

        public LoginSession()
        {
        }

        public LoginSession(DateTime? startTime, DateTime? endTime) : this()
        {
            Validate();

            this.StartTime = startTime;
            this.EndTime = endTime;
        }

        private static void Validate()
        {
            
        }

        public static LoginSession Create(DateTime? startTime, DateTime? endTime)
        {
            return new LoginSession(startTime,endTime);
        }

        public void Update(DateTime? startTime, DateTime? endTime)
        {
            Validate();

            this.StartTime = startTime;
            this.EndTime = endTime;
        }
    }
}
