using CountriesAPI.Model;

namespace CountriesAPI.View
{
   internal interface ICountryView
   {
      void DisplayPicture(Image image, PictureBox pictureBox);
      void DisplayCountries(IEnumerable<(string, string)> countries);
      void DisplayRegions(IEnumerable<string> regions);
      void ShowLoading(bool show);
      public void DisplayCountryDetails(Country country);
      void Init();
      void UpdateStatus(string message);
   }
}
