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

        public MeteoItemViewModel(Location entry)
        {
            Entry = entry;
        }
    }
}