using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurionpro.ACE.Core.NinjectResolver.Configuration
{
    public class NinjectConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("", IsRequired = false, IsKey = false, IsDefaultCollection = true)]
        public NinjectModuleCollection Modules
        {
            get { return ((NinjectModuleCollection)(base[""])); }
            set { base[""] = value; }
        }    
    }
}
