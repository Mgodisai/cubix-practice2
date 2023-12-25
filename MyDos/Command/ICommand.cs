namespace MyDos.Command
{
   internal interface ICommand
   {
      public string[] Aliases { get; init; }
      public void Execute(DirectoryHandler directoryHandler, params string[] parameters);
      public string GetHelp();
   }
}
