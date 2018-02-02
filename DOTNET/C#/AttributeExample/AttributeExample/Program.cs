using System;
using System.Reflection;

namespace AttributeExample
{
    class Program
    {
        static void Main(string[] args)
        {
            TestAttrribut attribute = new TestAttrribut();

            MethodInfo[] methods = attribute.GetType().GetMethods();
            foreach(var method in methods)
            {
                //Console.WriteLine(method.Name);
                var methodAtrribute= method.GetCustomAttributes(typeof(NeedRefectoring),true).GetType().Name;
                Console.WriteLine(methodAtrribute);        
            }

        }
    }
}
