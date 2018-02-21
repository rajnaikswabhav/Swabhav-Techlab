using System.Collections.Generic;
using Techlabs.Euphoria.Kernel.Framework.ObjectResolvers.Ninject;

namespace Techlabs.Euphoria.Kernel.Framework
{
    public class ObjectLocator
    {
        private static readonly ObjectLocator _instance = new ObjectLocator();

        private IObjectResolver _objectResolver = new NinjectObjectResolver();

        private ObjectLocator()
        {
            // Private constructor for singleton pattern
        }

        public static ObjectLocator Instance
        {
            get
            {
                return _instance;
            }
        }
        
        public IObjectResolver ObjectResolver
        {
            get { return _objectResolver; }
        }

        public void Initialize(IObjectResolver resolver)
        {
            _objectResolver = resolver;
        }

        public T Get<T>() where T:class
        {
            return _objectResolver.Get<T>();
        }

        public IList<T> GetAll<T>() where T: class
        {
            return _objectResolver.GetAll<T>() ;
        }

        public T Get<T>(string registeredName) where T:class
        {
            return _objectResolver.Get<T>(registeredName);
        }
    }
}
