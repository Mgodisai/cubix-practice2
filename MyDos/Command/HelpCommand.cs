using MyDos.ExtensionMethods;

namespace MyDos.Command
{
   internal class HelpCommand : ICommand
   {
      private Dictionary<string, ICommand>? _commands;
      public string[] Aliases { get; init; }
      public void SetCommandList(Dictionary<string, ICommand> commandList)
      {
         _commands = commandList;
      }

      public void Execute(DirectoryHandler directoryHandler, params string[] parameters)
      {
         var param = parameters[0];

         if (string.IsNullOrEmpty(param))
         {
            if (_commands == null) return;
            ConsoleExtensions.WriteLineWarning("List of available commands, use 'help [commandName]' for more information.");
            ConsoleExtensions.WriteLineWarning(string.Join(", ", _commands.Keys));
         }
         else
         {
            if (_commands != null && _commands.TryGetValue(param, out var command))
            {
               ConsoleExtensions.WriteLineWarning($"{param} - {command.GetHelp()}");
            }
            else
            {
               ConsoleExtensions.WriteLineWarning($"No help available for '{param}'");
            }
         }
      }

      public string GetHelp()
      {
         string aliases = string.Join(", ", Aliases);
         return $"Provides help information for commands. Usage: Get-Help (aliases: {aliases}) for list of commands or Get-Help [commandName] for details";
      }
   }
}