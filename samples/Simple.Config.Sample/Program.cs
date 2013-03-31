using System;
using System.Linq;

namespace Simple.Config.Sample
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Simple.Config Sample - Start");
            Console.WriteLine();

            var configManager = ConfigManager.GetInstance();

            var sampleNamespace = configManager.GetNamespace("NConfig.Sample");

            Console.WriteLine("Namespace [xml]: " + sampleNamespace.Name);

            foreach (var property in sampleNamespace.Properties)
                Console.WriteLine("Property: [{0} = {1}]", property.Name, property.Value);

            Console.WriteLine();

            var iniConfigFile = configManager.Load("second.ini");
            var iniNamespace = iniConfigFile.Namespaces.First();

            Console.WriteLine("Namespace [ini]: " + iniNamespace.Name);
            foreach (var property in iniNamespace.Properties)
                Console.WriteLine("Property: [{0} = {1}]", property.Name, property.Value);

            Console.WriteLine();

            var propertyWithManyValues = iniNamespace.Properties[1];
            foreach (var value in propertyWithManyValues.Values)
                Console.WriteLine("Property: [{0} = {1}]", propertyWithManyValues.Name, value);

            Console.WriteLine();

            Console.WriteLine("Simple.Config Sample - End");
        }
    }
}
