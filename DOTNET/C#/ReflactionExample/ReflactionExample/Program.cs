using System;
using System.Reflection;

namespace ReflactionExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Type classType = typeof(Object);
            ConstructorInfo[] constructors = classType.GetConstructors();
            Console.WriteLine("Constructor is: "+constructors.Length);

            MethodInfo[] methods = classType.GetMethods();
            Console.WriteLine("Method is: "+methods.Length);

            FieldInfo[] fields = classType.GetFields();
            Console.WriteLine("Fields is: "+fields.Length);

            MemberInfo[] members = classType.GetMembers();
            Console.WriteLine("Members is: "+members.Length);
        }
    }
}
