using Acr.UserDialogs;
using AdvancedTimer.Forms.Plugin.Abstractions;
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
        private Location currentLocation;
        public MeteoListPage()
        {
            InitializeComponent();

            BindingContext = new MeteoListViewModel();
         
            GetWeathers();

            IAdvancedTimer timer = DependencyService.Get<IAdvancedTimer>();

            timer.initTimer(3000, updateCurrentLocation, true);

            timer.startTimer();
        }

        private void updateCurrentLocation(object sender, EventArgs e)
        {
      
            if(currentLocation == null)
            {
                currentLocation = new Location();
                //((MeteoListViewModel)BindingContext).Entries.Add(currentLocation);
            }
            Gelocator geo = new Gelocator();
            Task<Location> location = geo.GetLocation();
            location.Wait();
            currentLocation.Longitude = location.Result.Longitude;
            currentLocation.Latitude = location.Result.Latitude;
            GetWeatherAsyncFromCoord(currentLocation);
            ((MeteoListViewModel)BindingContext).Entries[0] = currentLocation;
          //  Debug.WriteLine(currentLocation.Name, "WEEEE");
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
                {
                    Weather temp = newLocation.Weather;
                    newLocation.Weather = null;
                    ((MeteoListViewModel)BindingContext).addAndSave(newLocation);
                    newLocation.Weather = temp;
                }
            }
        }

        void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                ((MeteoListViewModel)BindingContext).Entries = ((MeteoListViewModel)BindingContext).Entries;
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
          
            newWeather.temperature =( (double)weather["main"]["temp"])-273.15;
            newWeather.locationName = (string)weather["name"];
            newWeather.icon = "https://openweathermap.org/img/w/" + (string)JObject.Parse(content)["weather"][0]["icon"] + ".png";
        

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
            newWeather.temperature = ((double)weather["main"]["temp"])-273.15;
            newWeather.locationName = (string)weather["name"];
            newWeather.icon = "https://openweathermap.org/img/w/" + (string)JObject.Parse(content)["weather"][0]["icon"] + ".png";

            location.Name = newWeather.locationName;
            location.Weather = newWeather;
        }
    }
}
