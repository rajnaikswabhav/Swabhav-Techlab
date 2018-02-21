using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurionpro.ACE.Core.NinjectResolver.Configuration
{
    public class NinjectModuleCollection : ConfigurationElementCollection
    {
        internal const string ItemPropertyName = "module";

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMapAlternate; }
        }

        protected override string ElementName
        {
            get { return ItemPropertyName; }
        }

        protected override bool IsElementName(string elementName)
        {
            return (elementName == ItemPropertyName);
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new NinjectModuleElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((NinjectModuleElement)element).Type;
        }

        public override bool IsReadOnly()
        {
            return false;
        }
    }
}
