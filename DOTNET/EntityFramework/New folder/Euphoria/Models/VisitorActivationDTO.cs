using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Techlabs.Euphoria.API.Models
{
    public enum ActionType{
         Verify , Send
    }
    public class VisitorActivationDTO
    {
        public ActionType Action { get; set; }
        public String Code { get; set; }
    }
}