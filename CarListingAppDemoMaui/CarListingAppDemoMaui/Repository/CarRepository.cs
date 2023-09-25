using System;
using CarListingAppDemoMaui.Model;
using SQLite;

namespace CarListingAppDemoMaui.Repository
{
    public class CarRepository
    {
        SQLiteConnection connection;
        public string StatusMessage;

        public CarRepository(string dbPath) => Init(dbPath);

        private void Init(string dbPath)
        {
            if (connection != null)
                return;

            connection = new SQLiteConnection(dbPath);
            connection.CreateTable<Car>();
        }

        public List<Car> GetCars()
        {
            try
            {
                return connection.Table<Car>().ToList();
            }
            catch (Exception)
            {
                StatusMessage = "Failed to retrieve data.";
            }
            return new List<Car>();
        }

        public Car GetCar(int id)
        {
            try
            {
                return connection.Table<Car>().FirstOrDefault(q => q.Id == id);
            }
            catch (Exception)
            {
                StatusMessage = "Failed to retrieve data.";
            }
            return null;
        }

        public void DeleteCar(int id)
        {
            try
            {
                connection.Table<Car>().Delete(q => q.Id == id);
            }
            catch (Exception)
            {
                StatusMessage = "Failed to delete data.";
            }
        }

        public void AddCar(Car car)
        {
            try
            {
                if (car == null) throw new Exception("Invalid Car Record");
                var result = connection.Insert(car);
                StatusMessage = result == 0 ? "Insert Failed" : "Insert Successful";
            }
            catch (Exception)
            {
                StatusMessage = "Failed to Insert data.";
            }
        }
        public void UpdateCar(Car car)
        {
            try
            {
                if (car == null) throw new Exception("Invalid Car Record");
                var result = connection.Update(car);
                StatusMessage = result == 0 ? "Update Failed" : "Update Successful";
            }
            catch (Exception)
            {
                StatusMessage = "Failed to Update data.";
            }
        }
    }
}

