using Common;
using Polly;
using System.IO.Compression;

namespace Zipper
{
   internal class Program
   {
      static void Main(string[] args)
      {
         string targetPath = args.Length > 0 ? args[0] : Directory.GetCurrentDirectory();
         string targetZipFilePath = string.Empty;
         string tempZipFilePath = string.Empty;

         var retryPolicy = Policy
            .Handle<IOException>()
            .WaitAndRetry(new[]
            {
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(2),
                TimeSpan.FromSeconds(4),
                TimeSpan.FromSeconds(8)
            }, (exception, timeSpan, retryCount, context) =>
            {
               string message = $"Attempt {retryCount}: Retrying in {timeSpan.Seconds} seconds";
               MyLogger.LogError(message + ", " + exception.Message);
               Console.WriteLine(message);
               DeleteFileIfExists(targetZipFilePath);
               DeleteFileIfExists(tempZipFilePath);
            });

         var fallbackPolicy = Policy
            .Handle<IOException>()
            .Fallback(() =>
            {
               string message = "All retries failed. Handling the final error.";
               MyLogger.LogError(message);
               Console.WriteLine(message);
               DeleteFileIfExists(tempZipFilePath);
               DeleteFileIfExists(targetZipFilePath);

            });
         var combinedPolicy = Policy.Wrap(fallbackPolicy, retryPolicy);


         if (File.Exists(targetPath))
         {
            targetZipFilePath = Path.ChangeExtension(targetPath, ".zip");

            combinedPolicy.Execute(() =>
            {
               using (var zip = ZipFile.Open(targetZipFilePath, ZipArchiveMode.Update))
               {
                  zip.CreateEntryFromFile(targetPath, Path.GetFileName(targetPath));
               }
               Console.WriteLine($"Created zip file: {targetZipFilePath}");
            });
         }
         else if (Directory.Exists(targetPath))
         {
            combinedPolicy.Execute(() =>
            {
               string tempDirectory = Path.GetTempPath();
               string zipFileName = new DirectoryInfo(targetPath).Name + ".zip";

               tempZipFilePath = Path.Combine(tempDirectory, zipFileName);
               targetZipFilePath = Path.Combine(targetPath, zipFileName);

               ZipFile.CreateFromDirectory(targetPath, tempZipFilePath);
               
               DeleteFileIfExists(targetZipFilePath);

               File.Move(tempZipFilePath, targetZipFilePath);

               DeleteFileIfExists(tempZipFilePath);

               Console.WriteLine($"Created zip file: {targetZipFilePath}");
            });
         }
         else
         {
            Console.WriteLine("Invalid path provided.");
            return;
         }
      }

      static void DeleteFileIfExists(string filePath)
      {
         if (File.Exists(filePath))
         {
            try
            {
               File.Delete(filePath);
            }
            catch (Exception ex)
            {
               Console.WriteLine($"Failed to delete file '{filePath}': {ex.Message}");
               throw; 
            }
         }
      }
   }
}
