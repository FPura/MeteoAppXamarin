using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MeteoAppXamarin
{
    public class Database
    {

        readonly SQLiteAsyncConnection database;

        public Database()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TestSQLite.db3");
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Location>().Wait();
        }

        /*
         * Ritorna una lista con tutti gli items.
         */
        public Task<List<Location>> GetItemsAsync()
        {
            return database.Table<Location>().ToListAsync();
        }

        /*
         * Query con query SQL.
         */
        public Task<List<Location>> GetItemsWithWhere(int id)
        {
            return database.QueryAsync<Location>("SELECT * FROM [TestItem] WHERE [Id] =?", id);
        }

        /*
         * Query con LINQ.
         */
        public Task<Location> GetItemAsync(int id)
        {
            return database.Table<Location>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        /*
         * Salvataggio o update.
         */
        public Task<int> SaveItemAsync(Location item)
        {
            if (item.Id == 0) // esempio
                return database.UpdateAsync(item);

            return database.InsertAsync(item);
        }

        public Task<int> DeleteItemAsync(Location item)
        {
            return database.DeleteAsync(item);
        }
        /*
        private SQLiteAsyncConnection database;

        public Database()
        {
            var dbPath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "TestSQLite.db3");
            database = new SQLiteAsyncConnection(dbPath);
        }
        public Task<CreateTableResult> CreateTableAsync<Location>(CreateFlags createFlags = CreateFlags.None) where Location : new();*/
    }
}
