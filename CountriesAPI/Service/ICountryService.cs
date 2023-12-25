using CountriesAPI.Model;

namespace CountriesAPI.Service
{
   internal interface ICountryService
   {
      IEnumerable<(string, string)> GetAllCountryNames();
      IEnumerable<string> GetAllRegionNames();
      IEnumerable<(string, string)> GetCountryNamesByRegions(string[] regions);
      Task<Image> LoadImageFromUrlAsync(string url);
      Task<Country> GetCountryByCountryCodeAsync(string countryName);
      Task RefreshCountryCacheAsync();
   }
}
