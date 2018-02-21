using Aurionpro.ACE.Core.NinjectResolver.Configuration;
using Ninject;
using Ninject.Extensions.Logging.Log4net;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Techlabs.Euphoria.Kernel.Framework.ObjectResolvers.Ninject
{
    public class NinjectObjectResolver : IObjectResolver
    {
        private IKernel _kernel;

        private INinjectModule[] _modulesToLoad;

        public NinjectObjectResolver()
        {

        }

        public NinjectObjectResolver(INinjectModule[] modules)
        {
            _modulesToLoad = modules;
        }

        public IKernel Kernel
        {
            get
            {
                if(_kernel == null)
                {
                    _kernel = new StandardKernel(new NinjectSettings { LoadExtensions = true});

                    AddStandardModules(_kernel);

                    _kernel.Load(GetModules());
                }
                return _kernel;
            }
        }

        private void AddStandardModules(IKernel kernel)
        {
            //kernel.Load(new Log4NetModule());
        }

        private INinjectModule[] GetModules()
        {
            if (_modulesToLoad != null)
                return _modulesToLoad;

            var ninjectConfigSection = ConfigurationManager.GetSection("ninjectmodules") as NinjectConfigurationSection;
            if (ninjectConfigSection == null)
                return null;

            var configuredModules = new List<INinjectModule>();
            foreach (NinjectModuleElement moduleElement in ninjectConfigSection.Modules)
            {
                try
                {
                    var moduleType = Type.GetType(moduleElement.Type);
                    var module = Activator.CreateInstance(moduleType) as INinjectModule;
                    if (module != null)
                        configuredModules.Add(module);
                }
                catch (System.Exception)
                {
                }
            }

            return configuredModules.ToArray();
        }

        public T Get<T>() where T:class
        {
            var kernel = Kernel;
            return kernel.Get<T>();
        }


        public IList<T> GetAll<T>() where T : class
        {
            var kernel = Kernel;
            return kernel.GetAll<T>().ToList();
        }


        public T Get<T>(string name) where T : class
        {
            return Kernel.Get<T>(name);
        }
    }
}
