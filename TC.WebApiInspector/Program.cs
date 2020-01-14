using System;
using System.Linq;
using System.Reflection;
using TC.WebApiInspector.Inspectors;

namespace TC.WebApiInspector
{
    class Program
    {
        static void Main(string[] args)
        {
            RunInspectors();

            Console.WriteLine("Press any button to exit");
            Console.ReadKey();
        }

        static void RunInspectors()
        {
            var inspectorAssembly = Assembly.GetAssembly(typeof(IInspector));
            foreach(var typeInfo in inspectorAssembly.DefinedTypes)
            {
                if (typeInfo.ImplementedInterfaces.Contains(typeof(IInspector)))
                {
                    var inspectorInstance = inspectorAssembly.CreateInstance(typeInfo.FullName) as IInspector;

                    inspectorInstance.Inspect();
                }
            }
        }
    }
}
