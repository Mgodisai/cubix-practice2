using static VT.TextTransformer.TextTransformer;

namespace DecoderConsole
{
   internal class Program
   {
      static int Main(string[] args)
      {
         string keyString = "shayepib,u_fldntCowvmTrg!c";
         string revString = "c!grTmvwoCtndlf_u,bipeyahs";

         (string switchType, List<string> texts) result;
         try
         {
            result = ParseCommandLineArguments(args);
         }
         catch (Exception ex)
         {
            Console.Error.WriteLine(ex.Message);
            return -1;
         }

         if (result.texts.Count > 0)
         { 
            try
            {
               switch (result.switchType)
               {
                  case "-e":
                     Encoder(keyString, result.texts.ToArray()).ToList().ForEach(s => Console.WriteLine(s));
                     break;
                  case "-d":
                     Decoder(keyString, result.texts.ToArray()).ToList().ForEach(s => Console.WriteLine(s));
                     break;
                  default:
                     Console.Error.WriteLine("Invalid data");
                     break;

               }
            } catch (Exception ex)
            {
               Console.Error.WriteLine("Error during processing, try different text to process: "+ex);
            }

         }
         else
         {
            Console.Error.WriteLine("Missing the text from args to process");
            return -1;
         }
         return 0;
      }

      private static (string type, List<string> texts) ParseCommandLineArguments(string[] args)
      {
         (string type, List<string> texts) result;

         if (args.Length == 0)
         {
            throw new ArgumentException($"Add switch (-e or -d) and texts to process to the command!");
         }

         if (args[0] == "-e" || args[0] == "-d")
         {
            result = (args[0], new List<string>());
         }
         else
         {
            throw new ArgumentException($"Not valid switch: {args[0]}");
         }
         for (int i = 1; i < args.Length; i++)
         {
            result.texts.Add(args[i]);
         }
         return result;
      }
   }

}

