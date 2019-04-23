using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MeteoAppXamarin
{
    public class Weather
    {
        public string WeatherResourceImage { get; set; }
        public double Temperature { get; set; }
        public string Description { get; set; }
        public string LocationName { get; set; }
        public Image Bitmap { get; set; }

    }
}
