using MeteoAppXamarin;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

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

            Task<List<Location>> locs = locationsDB.GetItemsAsync();
            locs.Wait();
            foreach (var loc in locs.Result)
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
