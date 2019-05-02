using MeteoAppXamarin;
using System;

namespace MeteoApp
{
    public class MeteoItemViewModel : BaseViewModel
    {
        Location _entry;
        
        public Location Entry
        {
            get { return _entry;  }
            set
            {
                _entry = value;
                OnPropertyChanged();
            }
        }
        public Weather EntryWeather
        {
            get { return _entry.Weather; }
            set { }
        }
        public MeteoItemViewModel(Location entry)
        {
            Entry = entry;
        }
    }
}