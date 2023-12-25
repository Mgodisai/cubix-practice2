using System.Text;

namespace MyDos
{
   internal class DirectoryHandler
   {
      public bool IsRunning { get; private set; }
      private DirectoryInfo _currentDirectory;
      public DirectoryInfo CurrentDirectory
      {
         get
         {
            return _currentDirectory;
         }
         set
         {
            var dirInfo = value;
            if (dirInfo.Exists)
            {
               _currentDirectory = value;
            }
         }
      }
      public DirectoryHandler() { 
         IsRunning = true;
         _currentDirectory = new DirectoryInfo(Directory.GetCurrentDirectory());
      }

      public void Stop()
      {
         IsRunning = false;
      }

      public string ListCurrentDirectory()
      {
         var sb = new StringBuilder();

         if (!_currentDirectory.Exists)
         {
            sb.AppendLine($"Directory {_currentDirectory} does not exist.");
            return sb.ToString();
         }

         sb.AppendLine("[D] . - Current Directory");
         if (_currentDirectory.Parent != null)
         {
            sb.AppendLine("[D] .. - Parent Directory: " + _currentDirectory.Parent.FullName);
         }

         DirectoryInfo[] subDirs = _currentDirectory.GetDirectories().OrderBy(d => d.Name).ToArray();
         foreach (DirectoryInfo dir in subDirs)
         {
            sb.AppendLine($"[D] {dir.Name} - Created: {dir.CreationTime:g} - Last Modified: {dir.LastWriteTime:g}");
         }

         FileInfo[] files = _currentDirectory.GetFiles().OrderBy(f => f.Name).ToArray();
         foreach (FileInfo file in files)
         {
            sb.AppendLine($"[F] {file.Name} - Size: {file.Length} bytes - Created: {file.CreationTime:g} - Last Modified: {file.LastWriteTime:g}");
         }
         return sb.ToString();
      }

   }
}
