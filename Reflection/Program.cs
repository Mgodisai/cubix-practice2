using System.Reflection.PortableExecutable;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Reflection;

namespace Reflection
{
   internal class Program
   {
      const string AssemblyName = "MyDos";
      const string AssemblyPath = @"C:\Users\vargat\source\repos\Practice2\MyDos\bin\Debug\net6.0\MyDos.dll";
      const string pdbPath = @"C:\Users\vargat\source\repos\Practice2\MyDos\bin\Debug\net6.0\MyDos.pdb";
      static void Main()
      {
         Assembly assembly = Assembly.Load(AssemblyName);

         foreach (Type type in assembly.GetTypes())
         {
            Console.WriteLine($"Type: {type.FullName}");

            foreach (MethodInfo method in type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly))
            {
               string accessModifier = GetAccessModifier(method);
               string returnType = method.ReturnType.Name;
               string parameters = string.Join(", ", method.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}"));

               Console.WriteLine($"\tMethod: {method.Name}, Access: {accessModifier}, Return Type: {returnType}, Parameters: [{parameters}]");
            }
         }
            
            Console.WriteLine("\n---------Method Lengths--------");
            PrintMethodLengths();
      }

      static string GetAccessModifier(MethodInfo method)
      {
         if (method.IsPublic)
            return "public";
         else if (method.IsPrivate)
            return "private";
         else if (method.IsFamily)
            return "protected";
         else if (method.IsAssembly)
            return "internal";
         else if (method.IsFamilyOrAssembly)
            return "protected internal";
         else if (method.IsFamilyAndAssembly)
            return "private protected";
         else
            return "unknown";
      }

      static void PrintMethodLengths()
      {
         using (FileStream assemblyStream = new FileStream(AssemblyPath, FileMode.Open, FileAccess.Read, FileShare.Read))
         using (FileStream pdbStream = new FileStream(pdbPath, FileMode.Open, FileAccess.Read, FileShare.Read))
         using (var peReader = new PEReader(assemblyStream))
         {
            if (!peReader.HasMetadata)
            {
               Console.WriteLine("No metadata found in assembly.");
               return;
            }

            var metadataReader = peReader.GetMetadataReader();
            var pdbReaderProvider = MetadataReaderProvider.FromPortablePdbStream(pdbStream);
            var pdbReader = pdbReaderProvider.GetMetadataReader();

            int i = 1;
            foreach (var methodHandle in metadataReader.MethodDefinitions)
            {
               var method = metadataReader.GetMethodDefinition(methodHandle);
               var containingType = metadataReader.GetTypeDefinition(method.GetDeclaringType());
               var typeName = metadataReader.GetString(containingType.Name);
               var methodName = metadataReader.GetString(method.Name);

               var methodDebugInformationHandle = MetadataTokens.MethodDebugInformationHandle(i);

               var methodDebugInfo = pdbReader.GetMethodDebugInformation(methodDebugInformationHandle);
               var sequencePoints = methodDebugInfo.GetSequencePoints().ToArray();
               if (sequencePoints.Any())
               {
                  var firstPoint = sequencePoints.First(point => !point.IsHidden);
                  var lastPoint = sequencePoints.Last(point => !point.IsHidden);
                  Console.WriteLine($"Type: {typeName}, Method: {methodName}, Start line: {firstPoint.StartLine}, End line: {lastPoint.EndLine}, Long: {lastPoint.EndLine- firstPoint.StartLine}");
               }
               i++;
            }
         }
      }
   }
}
