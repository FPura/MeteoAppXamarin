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

            Location loc = new Location();
            loc.Name = "a";
            GetWeatherAsync(loc);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        void OnItemAdded(object sender, EventArgs e)
        {
            DisplayAlert("Messaggio", "Testo", "OK");
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

        private async Task GetWeatherAsync(Location location)
        {
            var httpClient = new HttpClient();
            string content = await httpClient.GetStringAsync("https://api.openweathermap.org/data/2.5/weather?q=" + location.Name+"&appid="+apiKey);

            JObject weather = JObject.Parse(content);

            Weather newWeather = new Weather();
            newWeather.description = (string) weather["weather"][0]["description"];
            newWeather.temperature = (double) weather["main"]["temp"];
            newWeather.locationName = (string) weather["name"];
            Image image = new Image();
            Stream stream = new MemoryStream(await httpClient.GetByteArrayAsync("https://openweathermap.org/img/w/" + (string)weather["weather"][0]["icon"] + ".png"));
            image.Source = ImageSource.FromStream(() => { return stream; });
            newWeather.bitmap = image;
            Debug.WriteLine(weather, "WEEEE");
        }
        private async Task GetWeatherAsync()
        {
            var httpClient = new HttpClient();
            var content = await httpClient.GetStringAsync("https://samples.openweathermap.org/data/2.5/weather?q=Locarno&appid=b6907d289e10d714a6e88b30761fae22");

            var weather = (string)JObject.Parse(content)["weather"][0]["main"];

            Debug.WriteLine("WEEEEEEEEEEEEEEEE", weather);
        }
    }
}
