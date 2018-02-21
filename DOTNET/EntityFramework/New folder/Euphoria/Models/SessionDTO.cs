using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techlabs.Euphoria.API.Models
{
    public class SessionDTO
    {
        public string StaffName { get; set; }
        public List<SessionTimeCountDTO> SessionTimeCountDTO { get; set; }
        public SessionDTO()
        {
            SessionTimeCountDTO = new List<SessionTimeCountDTO>();
        }
    }
    public class SessionTimeCountDTO
    {
        public string Time { get; set; }
        public int count { get; set; }
    }
}
