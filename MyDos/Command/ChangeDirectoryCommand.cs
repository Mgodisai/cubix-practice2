namespace MyDos.Command
{
   internal class ChangeDirectoryCommand : ICommand
   {
      public string[] Aliases { get; init; }

      public void Execute(DirectoryHandler directoryHandler, params string[] parameters)
      {
         if (parameters == null || parameters.Length == 0)
         {
            throw new ArgumentException("No directory specified.");
         }
         string path = parameters[0].Trim();

         if (path == "..")
         {
            HandleParentDirectory(directoryHandler);
         }

         else if (parameters[0] == ".")
         {
            directoryHandler.CurrentDirectory = directoryHandler.CurrentDirectory;
         }
         else
         {
            HandleChangeDirectory(directoryHandler, path);
         }
      }

      public string GetHelp()
      {
         string aliases = string.Join(", ", Aliases);
         return $"Change the current directory. Usage cd (aliases: {aliases}) relative or absolute path";
      }

      private void HandleParentDirectory(DirectoryHandler directoryHandler)
      {
         if (directoryHandler.CurrentDirectory.Parent != null)
         {
            directoryHandler.CurrentDirectory = directoryHandler.CurrentDirectory.Parent;
         }
      }

      private void HandleChangeDirectory(DirectoryHandler directoryHandler, string path)
      {
         DirectoryInfo newDirectory;

         if (Path.IsPathRooted(path))
         {
            newDirectory = new DirectoryInfo(HandleRootPath(path));
         }
         else
         {
            newDirectory = new DirectoryInfo(Path.Combine(directoryHandler.CurrentDirectory.FullName, path));
         }

         if (newDirectory.Exists)
         {
            directoryHandler.CurrentDirectory = newDirectory;
         }
         else
         {
            throw new ArgumentException("Directory does not exist.");
         }
      }

      private string HandleRootPath(string path)
      {
         if (path.Length == 2 && path[1] == ':')
         {
            return path + "\\";
         }
         return path;
      }
   }
}
