using Common;
using MyDos.ExtensionMethods;

namespace MyDos.Command
{
   public class CommandHandler
   {
      private readonly Dictionary<string, ICommand> _commands;

      public CommandHandler()
      {
         ICommand helpCommand = new HelpCommand() { Aliases = new[] { "help", "Get-Help", "?" } };
         var commandsList = new List<ICommand>()
        {
            new ChangeDirectoryCommand() {Aliases = new[] { "cd", "Change-Directory" } },
            new ListDirectoryCommand()  { Aliases = new[] { "dir", "List-Directory", "ls" } },
            new QuitCommand()  { Aliases = new[] { "quit", "exit", "q" } },
            helpCommand,
        };


         _commands = new Dictionary<string, ICommand>();

         foreach (var command in commandsList)
         {
            foreach (var commandName in command.Aliases)
            {
               _commands[commandName.ToLower()] = command;
            }
         }
          ((HelpCommand)helpCommand).SetCommandList(_commands);
      }

      internal void HandleCommand(DirectoryHandler directoryHandler, string fullCommandString)
      {
         MyLogger.LogInfo($"Command called: {fullCommandString}");
         var commandParts = fullCommandString.Split(' ');
         var commandName = commandParts[0].ToLower();
         string parameter;
         if (commandParts.Length > 1)
         {
            parameter = string.Join(' ', commandParts.Skip(1));
         }
         else
         {
            parameter = string.Empty;
         }

         if (_commands.TryGetValue(commandName, out var commandObject))
         {
            try
            {
               commandObject.Execute(directoryHandler, parameter);
            } catch (ArgumentException ex)
            {
               MyLogger.LogError(ex.Message);
               ConsoleExtensions.WriteLineError(ex.Message);
            }
         }
         else
         {
            string message = "Unknown command: " + fullCommandString;
            MyLogger.LogError(message);
            ConsoleExtensions.WriteLineError("Unknown command.");
         }
      }
   }
}
