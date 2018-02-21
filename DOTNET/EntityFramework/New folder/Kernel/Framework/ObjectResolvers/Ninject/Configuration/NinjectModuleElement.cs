using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurionpro.ACE.Core.NinjectResolver.Configuration
{
    public class NinjectModuleElement : ConfigurationElement
    {
        [ConfigurationProperty("type", IsRequired = true)]
        public string Type {
            get { return (string)this["type"]; }
            set { this["type"] = value; }
        }
    }
}
