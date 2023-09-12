using System;
namespace CarListingAppDemoMaui.Model
{
    public class Car : BaseEntity
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public string Vin { get; set; }
    }
}