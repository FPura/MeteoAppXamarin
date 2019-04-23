using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json.Linq;

namespace MeteoAppXamarin
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            GetWeatherAsync();
        }

        private async Task GetWeatherAsync()
        {
            var httpClient = new HttpClient();
            var content = await httpClient.GetStringAsync("https://samples.openweathermap.org/data/2.5/weather?q=London,uk&appid=b6907d289e10d714a6e88b30761fae22");

            var weather = (string)JObject.Parse(content)["weather"][0]["main"];


        }
    }
}
