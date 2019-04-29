using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace MeteoAppXamarin
{
    public class Gelocator
    {
        public async Task<Location> GetLocation()
        {
            var locator = CrossGeolocator.Current; // singleton
            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));
            Debug.WriteLine("Position Status: {0}", position.Timestamp);
            Debug.WriteLine("Position Latitude: {0}", position.Latitude);
            Debug.WriteLine("Position Longitude: {0}", position.Longitude);
            Location currentLocation = new Location();
            currentLocation.Latitude = position.Latitude;
            currentLocation.Longitude = position.Longitude;
            return currentLocation;
        }
    }
}
