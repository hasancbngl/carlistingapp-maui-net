using System;
namespace CarListingAppDemoMaui.Model
{
    public class CarResponse
    {
        public List<Car> data { get; set; }
        public bool success { get; set; }
        public string message { get; set; }
    }
}

