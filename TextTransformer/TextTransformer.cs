namespace VT.TextTransformer
{
   public static class TextTransformer
   {
      /// <summary>
      /// Decode text parameters using keystring
      /// </summary>
      /// <param name="keyString">The keystring for decoding</param>
      /// <param name="textsToDecode">Array of texts to decode</param>
      /// <exception cref="ArgumentException">Thrown if keystring is null or when the textsToDecode is empty</exception>
      public static string[] Decoder(string keyString, params string[] textsToDecode)
      {
         if (keyString is null || textsToDecode.Length == 0)
         {

            throw new ArgumentException("Not valid arguments!");
         }
         var key = keyString.Select((c, i) => (c, i)).ToDictionary(e => e.c, e => e.i);
         var lookup = new string(keyString.Reverse().ToArray());

         string Decode(string encodedText)
         {
            return new string(encodedText.Select(c => keyString[lookup.IndexOf(c)]).ToArray());
         }

         var result = textsToDecode
            .Select(s => Decode(s))
            .ToArray();

         return result;
      }

      /// <summary>
      /// Encode text parameters using the keystring
      /// </summary>
      /// <param name="keyString">The keystring for encoding</param>
      /// <param name="textsToEncode">Array of texts to encode</param>
      /// <exception cref="ArgumentException">Thrown if keystring is null or when the textsToDecode is empty</exception>
      public static string[] Encoder(string keyString, params string[] textsToEncode)
      {
         if (keyString is null || textsToEncode.Length == 0)
         {

            throw new ArgumentException("Not valid arguments!");
         }

         var key = keyString.Select((c, i) => (c, i)).ToDictionary(e => e.c, e => e.i);
         var lookup = new string(keyString.Reverse().ToArray());

         string Encode(string plainText)
         {
            return new string(plainText.Select(c => lookup[key[c]]).ToArray());
         }

         var result = textsToEncode
            .Select(s => Encode(s))
            .ToArray();

         return result;
      }
   }
}
