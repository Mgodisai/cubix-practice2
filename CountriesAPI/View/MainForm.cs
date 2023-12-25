using Common;
using CountriesAPI.Model;
using CountriesAPI.Presenter;
using CountriesAPI.View;
using System.Data;

namespace CountriesAPI
{
   internal partial class MainForm : Form, ICountryView
   {
      private readonly CountryPresenter _presenter;

      public MainForm()
      {
         InitializeComponent();
         _presenter = new CountryPresenter(this);
      }

      public void DisplayCountries(IEnumerable<(string, string)> countries)
      {
         if (InvokeRequired)
         {
            Invoke(new Action<IEnumerable<(string, string)>>(DisplayCountries), countries);
            return;
         }
         CountriesListBox.DataSource = null;
         CountriesListBox.DataSource = countries
          .Select(c => new { CCode = c.Item1, CName = c.Item2 })
          .OrderBy(c => c.CName)
          .ToList();
         CountriesListBox.DisplayMember = "CName";
         CountriesListBox.ValueMember = "CCode";
         CountriesListBox.Refresh();
      }


      public void DisplayRegions(IEnumerable<string> regions)
      {
         if (InvokeRequired)
         {
            Invoke(new Action<IEnumerable<string>>(DisplayRegions), regions);
            return;
         }
         RegionsCheckedListBox.DataSource = regions.OrderBy(r => r).ToList();
         RegionsCheckedListBox.Refresh();
      }

      public void DisplayCountryDetails(Country country)
      {
         DataTable table = new();
         table.Columns.Add("Property");
         table.Columns.Add("Value");

         table.Rows.Add("CommonName", country.Name.Common);
         table.Rows.Add("OfficialName", country.Name.Official);
         table.Rows.Add("Native Name", country.Name.NativeNames is null ? "Unknown" : FormatNativeNames(country.Name.NativeNames));
         table.Rows.Add("Capital", country.Capital is null ? "Unknown" : string.Join(", ", country.Capital));
         table.Rows.Add("Region", country.Region);
         table.Rows.Add("Subregion", country.Subregion);
         table.Rows.Add("Area", country.Area.ToString("N0") + " km2");
         table.Rows.Add("Population", country.Population.ToString("N0"));
         table.Rows.Add("CC", country.Cca2);
         table.Rows.Add("IOC Code", country.Cioc);
         table.Rows.Add("tld", country.Tld is null ? string.Empty : string.Join(", ", country.Tld));
         table.Rows.Add("Currencies", country.Currencies is null ? "Unknown" : FormatCurrencies(country.Currencies));
         table.Rows.Add("Car sign", country.CarInfo?.Signs is null ? "Unknown" : string.Join(", ", country.CarInfo.Signs));
         table.Rows.Add("Languages", FormatLanguages(country.Languages));
         table.Rows.Add("Google Map", country.Maps.GMaps);
         table.Rows.Add("OSM", country.Maps.Osm);

         CountryDataGridView.DataSource = table;
         CountryDataGridView.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
         CountryDataGridView.Refresh();
      }

      private async void MainForm_Load(object sender, EventArgs e)
      {
         try
         {
            await _presenter.Init();


         }
         catch (Exception ex)
         {
            MyLogger.LogError(ex.Message);
         }
      }

      private void CountriesListBox_SelectedIndexChanged(object sender, EventArgs e)
      {

         string? selectedCountry = CountriesListBox.SelectedValue as string;
         if (selectedCountry != null)
         {
            _presenter.LoadFlag(selectedCountry, CoaPictureBox).ConfigureAwait(false);
            _presenter.LoadCoatOfArm(selectedCountry, FlagPictureBox).ConfigureAwait(false);
            _presenter.DisplayCountryDetails(selectedCountry).ConfigureAwait(false);
         }

      }

      public void ShowLoading(bool show)
      {
         if (InvokeRequired)
         {
            Invoke(new Action<bool>(ShowLoading), show);
            return;
         }

         ToolStripProgressBar.Visible = show;

      }

      public void DisplayPicture(Image image, PictureBox pictureBox)
      {
         if (InvokeRequired)
         {
            Invoke(new Action<Image, PictureBox>(DisplayPicture), image, pictureBox);
            return;
         }
         pictureBox.Image = image;
      }

      private void RegionsCheckedListBox_SelectedIndexChanged(object sender, EventArgs e)
      {
         List<string> selectedItems = new List<string>();
         foreach (var item in RegionsCheckedListBox.CheckedItems)
         {
            if (item != null)
            {
               selectedItems.Add(item.ToString());
            }
         }

         try
         {
            _presenter.LoadFilteredCountries(selectedItems.ToArray());
         }
         catch (Exception ex)
         {
            MyLogger.LogError(ex.Message);
         }
      }

      public void Init()
      {
         CoaPictureBox.Image = DefaultImageResources.shield_256;
         FlagPictureBox.Image = DefaultImageResources.flag_256;
         CountryDataGridView.ShowEditingIcon = false;
         CountryDataGridView.AllowUserToResizeColumns = true;
         CountryDataGridView.AllowUserToOrderColumns = false;
         CountryDataGridView.AllowUserToDeleteRows = false;
         CountryDataGridView.AllowUserToResizeRows = false;
         CountryDataGridView.AllowUserToAddRows = false;
         CountryDataGridView.ReadOnly = true;
         CountryDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
         CountryDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
         CountryDataGridView.AutoResizeColumns();
         CountryDataGridView.ScrollBars = ScrollBars.None;
         CountryDataGridView.RowHeadersVisible = false;
         CountryDataGridView.ColumnHeadersHeight = 40;
         CountryDataGridView.AlternatingRowsDefaultCellStyle.BackColor = Color.LightSlateGray;
         CountryDataGridView.DefaultCellStyle.Font = new Font("Segoe UI Emoji", 10F);
      }

      public void UpdateStatus(string message)
      {
         if (InvokeRequired)
         {
            Invoke(new Action<string>(UpdateStatus), message);
            return;
         }
         ToolStripLabel.Text = message;
      }

      private string FormatCurrencies(Dictionary<string, Currency> currencies)
      {
         List<string> formattedCurrencies = new List<string>();
         foreach (var currency in currencies)
         {
            string code = currency.Key;
            Currency details = currency.Value;
            formattedCurrencies.Add($"{details.Name} ({code}) - Symbol: {details.Symbol}");
         }
         return string.Join(", ", formattedCurrencies);
      }

      private string FormatLanguages(Dictionary<string, string> languages)
      {
         return string.Join(", ", languages.Select(lang => $"{lang.Value} ({lang.Key})"));
      }

      private string FormatNativeNames(Dictionary<string, NativeName> nativeNames)
      {
         return string.Join(", ", nativeNames.Select(nn => $"{nn.Value.Common} - {nn.Value.Official} ({nn.Key})"));
      }

      private void CountryDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
      {
         if (e.ColumnIndex == 1 && e.RowIndex >= 0)
         {
            string cellValue = CountryDataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString() ?? "";
            if (cellValue.StartsWith("https:"))
            {
               System.Diagnostics.Process.Start("explorer.exe", cellValue);
            }
         }
      }
   }
}
