namespace MyDos.Command
{
   internal class QuitCommand : ICommand
   {
      public string[] Aliases { get; init; } = new string[0];

      public void Execute(DirectoryHandler directoryHandler, params string[] parameters)
      {
         directoryHandler.Stop();
      }

      public string GetHelp()
      {
         string aliases = string.Join(", ", Aliases);
         return $"Exits the app. Usage: quit (aliases: {aliases})";
      }
   }
}
