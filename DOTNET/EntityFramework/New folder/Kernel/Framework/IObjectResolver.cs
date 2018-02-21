using System.Collections.Generic;

namespace Techlabs.Euphoria.Kernel.Framework
{
    public interface IObjectResolver
    {
        T Get<T>() where T: class;
        T Get<T>(string name) where T:class;
        IList<T> GetAll<T>() where T : class; 
    }
}
