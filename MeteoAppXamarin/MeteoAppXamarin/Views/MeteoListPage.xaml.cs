using Acr.UserDialogs;
using MeteoAppXamarin;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MeteoApp
{
    public partial class MeteoListPage : ContentPage
    {
        private string apiKey = "815504bb440299e3ebbb76868cbc7c47";

        public MeteoListPage()
        {
            InitializeComponent();

            BindingContext = new MeteoListViewModel();

            GetWeathers();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        void OnItemAdded(object sender, EventArgs e)
        {

            addLocation();
    
        }

        private async Task addLocation()
        {
            PromptResult pResult = await UserDialogs.Instance.PromptAsync(new PromptConfig
            {
                InputType = InputType.Default,
                OkText = "Add",
                Title = "Add location",
            });

            if (pResult.Ok && !string.IsNullOrWhiteSpace(pResult.Text))
            {
                Location newLocation = new Location();
                newLocation.Name = pResult.Text;
                await GetWeatherAsyncFromName(newLocation);
                if (newLocation.Weather != null)
                    ((MeteoListViewModel)BindingContext).addAndSave(newLocation);
            }
        }

        void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                Navigation.PushAsync(new MeteoItemPage()
                {
                    BindingContext = new MeteoItemViewModel(e.SelectedItem as Location)
                });
            }
        }

        private async Task GetWeathers()
        {
            foreach (var location in ((MeteoListViewModel)BindingContext).Entries)
            {
                await GetWeatherAsyncFromName(location);
            }
        }
        private async Task GetWeatherAsyncFromName(Location location)
        {
            var httpClient = new HttpClient();
            string content = await httpClient.GetStringAsync("https://api.openweathermap.org/data/2.5/weather?q=" + location.Name + "&appid=" + apiKey);

            JObject weather = JObject.Parse(content);

            Weather newWeather = new Weather();
            newWeather.description = (string)weather["weather"][0]["description"];
            newWeather.temperature = (double)weather["main"]["temp"];
            newWeather.locationName = (string)weather["name"];
            Image image = new Image();
            Stream stream = new MemoryStream(await httpClient.GetByteArrayAsync("https://openweathermap.org/img/w/" + (string)weather["weather"][0]["icon"] + ".png"));
            image.Source = ImageSource.FromStream(() => { return stream; });
            newWeather.bitmap = image;

            location.Name = newWeather.locationName;
            location.Weather = newWeather;
            //  Debug.WriteLine(weather, "WEEEE");
        }
        private async Task GetWeatherAsyncFromCoord(Location location)
        {
            var httpClient = new HttpClient();
            string content = await httpClient.GetStringAsync("https://api.openweathermap.org/data/2.5/weather?lon=" + location.Longitude + "&lat="+ location.Latitude +"&appid=" + apiKey);

            JObject weather = JObject.Parse(content);

            Weather newWeather = new Weather();
            newWeather.description = (string)weather["weather"][0]["description"];
            newWeather.temperature = (double)weather["main"]["temp"];
            newWeather.locationName = (string)weather["name"];
            Image image = new Image();
            Stream stream = new MemoryStream(await httpClient.GetByteArrayAsync("https://openweathermap.org/img/w/" + (string)weather["weather"][0]["icon"] + ".png"));
            image.Source = ImageSource.FromStream(() => { return stream; });
            newWeather.bitmap = image;

            location.Weather = newWeather;
        }
    }
}
