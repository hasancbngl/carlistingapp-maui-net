using System;
using CarListingAppDemoMaui.Model;

namespace CarListingAppDemoMaui.Service
{
    public class CarRepository
    {
        public List<Car> GetCars()
        {
            return new List<Car>()
            {
                new Car
                {
                    Id = 1,
                    Make = "Honda",
                    Model = "Fit",
                    Vin ="123",
                },
                new Car
                {
                    Id = 2,
                    Make = "Toyota",
                    Model = "Prado",
                    Vin ="1234",
                },
                new Car
                {
                    Id = 3,
                    Make = "Honda",
                    Model = "Civic",
                    Vin ="123",
                },
                 new Car
                {
                    Id = 4,
                    Make = "BMW",
                    Model = "M3",
                    Vin ="123e",
                },
                  new Car
                {
                    Id = 5,
                    Make = "Ferrari",
                    Model = "Spider",
                    Vin ="123s",
                },
            };
        }
    }
}

