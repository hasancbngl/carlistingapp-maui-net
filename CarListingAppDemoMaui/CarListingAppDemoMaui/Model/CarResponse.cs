using System;
using static Microsoft.Maui.Controls.Internals.Profile;

namespace CarListingAppDemoMaui.Model
{
    public class CarResponse
    {
        public List<Car> data { get; set; }
        public bool success { get; set; }
        public string message { get; set; }
    }
}

