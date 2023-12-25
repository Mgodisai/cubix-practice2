using Common;
using CountriesAPI.Service;
using CountriesAPI.View;

namespace CountriesAPI.Presenter
{
   internal class CountryPresenter
   {
      private readonly ICountryView _view;
      private readonly CountryService _service;

      public CountryPresenter(ICountryView view)
      {
         this._view = view;
         this._service = new CountryService();
      }

      public async Task Init()
      {
         await _service.RefreshCountryCacheAsync();
         LoadRegions();
         _view.Init();
      }

      public void LoadRegions()
      {
         try
         {

            _view.ShowLoading(true);
            var regions = _service.GetAllRegionNames();
            _view.DisplayRegions(regions);
            _view.UpdateStatus("Regions loaded successfully.");
         }
         catch (Exception ex)
         {
            _view.UpdateStatus($"Failed to load: {ex.Message}");
            MyLogger.LogError(ex.Message);
         }
         finally
         {
            _view.ShowLoading(false);
         }
      }

      public async Task LoadFlag(string countryCode, PictureBox pictureBox)
      {
         try
         {
            _view.ShowLoading(true);
            var country = await _service.GetCountryByCountryCodeAsync(countryCode);
            var picture = await _service.LoadImageFromUrlAsync(country.Flags.Png);
            if (picture is null)
            {
               picture = DefaultImageResources.flag_256;
            }

            _view.DisplayPicture(picture, pictureBox);


            _view.UpdateStatus("Flag pic loaded successfully.");
         } catch (Exception ex)
         {
            _view.UpdateStatus($"Failed to load flag: {ex.Message}");
            _view.DisplayPicture(DefaultImageResources.flag_256, pictureBox);
         }
         finally
         {
            _view.ShowLoading(false);
         }
      }

      public async Task LoadCoatOfArm(string countryCode, PictureBox pictureBox)
      {
         try
         {
            _view.ShowLoading(true);
            var country = await _service.GetCountryByCountryCodeAsync(countryCode);
            var picture = await _service.LoadImageFromUrlAsync(country.CoatOfArms.Png);
            if (picture is null)
            {
               picture = DefaultImageResources.shield_256;
            }
            _view.DisplayPicture(picture, pictureBox);
            _view.UpdateStatus("Coat of Arms pic loaded successfully.");
         }
         catch (Exception ex)
         {
            _view.UpdateStatus($"Failed to load Coat of Arms: {ex.Message}");
            _view.DisplayPicture(DefaultImageResources.shield_256, pictureBox);
         }
         finally
         {
            _view.ShowLoading(false);
         }
      }

      public void LoadFilteredCountries(string[] regions)
      {
         try
         {
            _view.ShowLoading(true);
            var countries = _service.GetCountryNamesByRegions(regions);
            _view.DisplayCountries(countries);
            _view.UpdateStatus("Filtered country-list loaded successfully.");
         }
         catch (Exception ex)
         {
            _view.UpdateStatus($"Failed to load filtered country list: {ex.Message}");
         }
         finally
         {
            _view.ShowLoading(false);
         }
      }

      public async Task DisplayCountryDetails(string countryCode)
      {
         try
         {
            _view.ShowLoading(true);
            var country = await _service.GetCountryByCountryCodeAsync(countryCode);
            _view.DisplayCountryDetails(country);
            _view.UpdateStatus("Country details loaded successfully.");
         }
         catch (Exception ex)
         {
            _view.UpdateStatus($"Failed to load country details: {ex.Message}");
         }
         finally
         {
            _view.ShowLoading(false);
         }
      }
   }
}
