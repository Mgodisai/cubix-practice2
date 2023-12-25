using MyDos.ExtensionMethods;

namespace MyDos.Command
{
   internal class ListDirectoryCommand : ICommand
   {
      public string[] Aliases { get; init; }
      public void Execute(DirectoryHandler directoryHandler, params string[] parameters)
      {
         ConsoleExtensions.WriteLineSuccess(directoryHandler.ListCurrentDirectory());
      }

      public string GetHelp()
      {
         string aliases = string.Join(", ", Aliases);
         return $"List current Directory. Usage: dir (aliases: {aliases})";
      }
   }
}