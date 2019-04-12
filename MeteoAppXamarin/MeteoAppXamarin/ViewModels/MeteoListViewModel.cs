using MeteoAppXamarin;
using System;
using System.Collections.ObjectModel;

namespace MeteoApp
{
    public class MeteoListViewModel : BaseViewModel
    {
        ObservableCollection<Location> _entries;

        public ObservableCollection<Location> Entries
        {
            get { return _entries; }
            set
            {
                _entries = value;
                OnPropertyChanged();
            }
        }

        public MeteoListViewModel()
        {
            Entries = new ObservableCollection<Location>();

            for (var i = 0; i < 10; i++)
            {
                var e = new Location
                {
                    Id = i,
                    Name = "Entry " + i
                };

                Entries.Add(e);
            }
        }
    }
}
