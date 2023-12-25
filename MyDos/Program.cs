using MyDos.Command;

namespace MyDos
{
   internal class Program
   {
      static void Main(string[] args) 
      {
         var commandHandler = new CommandHandler();
         var directoryHandler = new DirectoryHandler();

         while (directoryHandler.IsRunning)
         {
            Console.Write(directoryHandler.CurrentDirectory + "> ");
            var input = Console.ReadLine() ?? string.Empty;
            commandHandler.HandleCommand(directoryHandler, input);
         }
      }
   }
}
