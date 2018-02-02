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
                var attr = method.GetCustomAttributes(typeof(NeedRefectoring),true);
                Console.WriteLine(attr.GetType().Name);
            }

        }
    }
}
