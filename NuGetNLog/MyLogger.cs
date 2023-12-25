using NLog;
using NLog.Config;
using System.Runtime.CompilerServices;

namespace Common
{
   public static class MyLogger
   {
      private static readonly Logger logger = LogManager.GetCurrentClassLogger();
      private const string loggerConfig = "nlog.config";

      static MyLogger()
      {
         try
         {
            if (File.Exists(loggerConfig))
            {
               var config = new XmlLoggingConfiguration(loggerConfig);
               LogManager.Configuration = config;
               LogInfo("Logger configuration loaded successfully.");
            }
            else
            {
               LogInternalError($"Logger configuration file '{loggerConfig}' not found.");
            }
         }
         catch (Exception ex)
         {
            LogInternalError($"Error loading logger configuration: {ex.Message}");
         }
      }

      private static void LogInternalError(string message)
      {
         Console.Error.WriteLine($"[Logger Internal Error] {message}");
      }


      public static void LogDebug(string message, [CallerMemberName] string memberName = "",
                                 [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
      {
         string logMessage = $"{filePath}({lineNumber}): {memberName} - {message}";
         logger.Debug(logMessage);
      }

      public static void LogInfo(string message, [CallerMemberName] string memberName = "",
                                  [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
      {
         string logMessage = $"{filePath}({lineNumber}): {memberName} - {message}";
         logger.Info(logMessage);
      }

      public static void LogWarning(string message, [CallerMemberName] string memberName = "",
                            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
      {
         string logMessage = $"{filePath}({lineNumber}): {memberName} - {message}";
         logger.Warn(logMessage);
      }

      public static void LogError(string message)
      {
         logger.Error(message);
      }

      public static void LogError(Exception ex, string message)
      {
         logger.Error(ex, message);
      }

      public static void LogFatal(string message)
      {
         logger.Fatal(message);
      }

      public static void LogFatal(Exception ex, string message)
      {
         logger.Fatal(ex, message);
      }
   }
}
