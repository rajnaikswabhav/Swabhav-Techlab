using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techlabs.Euphoria.Kernel.Service.Email
{
    public interface IEmailService
    {
         void Send(string visitorEmailId, string subject, string body, string attachementFile = null);     
    }
}
