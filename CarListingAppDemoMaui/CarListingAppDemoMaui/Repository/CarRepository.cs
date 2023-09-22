using System;
using CarListingAppDemoMaui.Model;
using SQLite;

namespace CarListingAppDemoMaui.Repository
{
    public class CarRepository
    {
        SQLiteConnection connection;
        string _dbPath;
        public string StatusMessage;

        public CarRepository(string dbPath)
        {
            _dbPath = dbPath;
        }

        private void Init()
        {
            if (connection != null) return;
            connection = new SQLiteConnection(_dbPath);
            connection.CreateTable<Car>();
        }

        public List<Car> GetCars()
        {
            try
            {
                Init();
                return connection.Table<Car>().ToList();
            }
            catch (Exception)
            {
                StatusMessage = "Failed to retrieve data.";
            }
            return new List<Car>();
        }
    }
}

