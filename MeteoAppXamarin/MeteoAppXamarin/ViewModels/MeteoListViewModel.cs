using MeteoAppXamarin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MeteoApp
{
    public class MeteoListViewModel : BaseViewModel
    {


        ObservableCollection<Location> _entries;


        public static Database locationsDB = new Database();

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
            Location location = new Location();
            location.Id = 1;
            location.Name = "locarno";
            locationsDB.SaveItemAsync(location);
            List<Location> locs = locationsDB.GetItemsAsync().Result;
            foreach (var loc in locs)
            {
               /* var e = new Location
                {
                    Id = i,
                    Name = "Entry " + i
                };*/

                Entries.Add(loc);
            }
        }

        public void addAndSave(Location location)
        {
            location.Id = Entries.Count + 1;
            locationsDB.SaveItemAsync(location);
            Entries.Add(location);
        }
    }
}
