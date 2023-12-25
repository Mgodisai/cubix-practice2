using System.Text.Json.Serialization;

namespace CountriesAPI.Model
{
   public record Country(
      [property: JsonPropertyName("name")] Name Name,
      [property: JsonPropertyName("capital")] List<string>? Capital,
      [property: JsonPropertyName("cca2")] string Cca2,
      [property: JsonPropertyName("cioc")] string Cioc,
      [property: JsonPropertyName("coatOfArms")] CoatOfArms? CoatOfArms,
      [property: JsonPropertyName("flags")] Flags Flags,
      [property: JsonPropertyName("region")] string Region,
      [property: JsonPropertyName("subregion")] string? Subregion,
      [property: JsonPropertyName("area")] double Area,
      [property: JsonPropertyName("tld")] List<string>? Tld,
      [property: JsonPropertyName("population")] long Population,
      [property: JsonPropertyName("languages")] Dictionary<string, string> Languages,
      [property: JsonPropertyName("maps")] Maps Maps,
      [property: JsonPropertyName("currencies")] Dictionary<string, Currency>? Currencies,
      [property: JsonPropertyName("car")] Car? CarInfo
   );

   public record Name(
      [property: JsonPropertyName("common")] string Common,
      [property: JsonPropertyName("official")] string Official,
      [property: JsonPropertyName("nativeName")] Dictionary<string, NativeName>? NativeNames
   );

   public record CoatOfArms(
       [property: JsonPropertyName("png")] string? Png,
       [property: JsonPropertyName("svg")] string? Svg
   );

   public record Flags(
      [property: JsonPropertyName("png")] string Png
   );

   public record Currency(
      [property: JsonPropertyName("name")] string Name,
      [property: JsonPropertyName("symbol")] string Symbol
   );

   public record Maps(
      [property: JsonPropertyName("googleMaps")] string GMaps,
      [property: JsonPropertyName("openStreetMaps")] string Osm
   );

   public record NativeName(
      [property: JsonPropertyName("common")] string Common,
      [property: JsonPropertyName("official")] string Official
   );

   public record Car(
      [property: JsonPropertyName("signs")] List<string>? Signs,
      [property: JsonPropertyName("side")] string? Side
   );
}

