using MyFirstFramework;
using static System.Console;

namespace MyFirstFrameworkApp
{
   internal class Program
   {
      static void Main()
      {
         new Random()
            .If(r => r.Next(2) == 0)
            .Then(ifTrue: () => WriteLine("Bal"), ifFalse: () => WriteLine("Jobb"));
      }
   }
}
