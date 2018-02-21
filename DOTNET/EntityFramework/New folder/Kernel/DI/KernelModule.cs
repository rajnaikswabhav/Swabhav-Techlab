using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techlabs.Euphoria.Kernel.Framework.Repository;
using Techlabs.Euphoria.Kernel.Framework.Repository.EntityFramework;
using Techlabs.Euphoria.Kernel.Model;

namespace Techlabs.Euphoria.Kernel.DI
{
    public class KernelModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRepository<Organizer>>().To<EntityFrameworkRepository<Organizer>>().InSingletonScope();
        }
    }
}
