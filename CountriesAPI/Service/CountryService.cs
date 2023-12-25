using System.Collections.Concurrent;
using System.Text.Json;
using Common;
using CountriesAPI.Model;

namespace CountriesAPI.Service
{
   internal class CountryService : ICountryService
   {
      private static readonly HttpClient _httpClient = new();
      private const string BaseUrl = "https://restcountries.com/v3.1";
      private readonly ConcurrentDictionary<string, CacheItem<Country>> _countryCache = new();
      private readonly TimeSpan _cacheLifespan = TimeSpan.FromMinutes(2);
      private List<(string, string)> CountryNames 
         => _countryCache
         .Select(cc => (cc.Key, cc.Value.Data.Name.Common))
         .ToList();

      private List<string> Regions 
         => _countryCache
         .Select(cc => cc.Value.Data.Region)
         .Distinct()
         .ToList();

      public async Task RefreshCountryCacheAsync()
      {
         try
         {
            string url = $"{BaseUrl}/all";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();

            var countries = JsonSerializer.Deserialize<IEnumerable<Country>>(jsonResponse);
            if (countries is not null)
            {
               foreach (var country in countries)
               {
                  _countryCache[country.Cca2] = new CacheItem<Country>() { Data = country, Timestamp = DateTime.UtcNow };
               }
            }
            else throw new Exception("Error retrieving data from the REST Countries API");

         }
         catch (HttpRequestException e)
         {
            throw new Exception("Error retrieving data from the REST Countries API", e);
         }
         catch (JsonException jsonEx)
         {
            MyLogger.LogError($"JSON Deserialization failed: {jsonEx.Message}");
            throw;
         }
         catch (Exception ex)
         {
            MyLogger.LogError($"Exception: {ex.Message}");
            throw;
         }
      }

      public IEnumerable<(string, string)> GetAllCountryNames()
      {
         return CountryNames;
      }

      public IEnumerable<string> GetAllRegionNames()
      {
         return Regions;
      }

      public IEnumerable<(string, string)> GetCountryNamesByRegions(string[] regions)
      {
         return _countryCache.Values
            .Select(v => v.Data)
            .Where(d => regions
            .Contains(d.Region))
            .Select(c => (c.Cca2, c.Name.Common))
            .ToList();
      }

      public async Task<Country> GetCountryByCountryCodeAsync(string countryCode)
      {

         if (_countryCache.TryGetValue(countryCode, out CacheItem<Country>? cachedItem))
         {
            if (cachedItem is not null && DateTime.UtcNow - cachedItem.Timestamp < _cacheLifespan)
            {
               return cachedItem.Data;
            }
         }
         try
         {
            var response = await _httpClient.GetAsync($"{BaseUrl}/alpha/{countryCode}");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var country = JsonSerializer.Deserialize<Country[]>(jsonResponse);
            if (country is not null && country.Length > 0)
            {
               _countryCache.AddOrUpdate(countryCode, new CacheItem<Country> { Data = country[0], Timestamp = DateTime.UtcNow },
                  (key, existingVal) => new CacheItem<Country> { Data = country[0], Timestamp = DateTime.UtcNow });

               MyLogger.LogInfo($"Cache was updated with country: {country[0].Name.Common}");
               return country[0];
            }
            else
            {
               if (_countryCache[countryCode].Data is not null)
               {
                  _countryCache.TryRemove(countryCode, out _);
                  throw new ArgumentException($"Country name is deprecated, removed from cache ({countryCode})");
               }
               else
               {
                  throw new ArgumentException($"Country does not exists with the given countryName ({countryCode})");
               }
            }
         }
         catch (HttpRequestException httpEx)
         {
            MyLogger.LogError($"Error retrieving data from the REST Countries API: {httpEx.Message}");
            throw;
         }
         catch (JsonException jsonEx)
         {
            MyLogger.LogError($"JSON Deserialization failed: {jsonEx.Message}");
            throw;
         }
         catch (Exception ex)
         {
            MyLogger.LogError($"Exception: {ex.Message}");
            throw;
         }
      }

      public async Task<Image> LoadImageFromUrlAsync(string url)
      {
         try
         {
            var response = await _httpClient.GetAsync(url);
            using var stream = await response.Content.ReadAsStreamAsync();
            return Image.FromStream(stream);
         }
         catch (HttpRequestException httpEx)
         {
            MyLogger.LogError($"Error retrieving data from the REST Countries API: {httpEx.Message}");
            throw;
         }
         catch (Exception ex)
         {
            MyLogger.LogError($"Exception: {ex.Message}");
            throw;
         }
      }
   }
}
