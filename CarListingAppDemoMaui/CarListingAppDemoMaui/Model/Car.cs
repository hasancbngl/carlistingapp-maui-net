using System;
using SQLite;

namespace CarListingAppDemoMaui.Model
{
    [Table("carsDb")]
    public class Car : BaseEntity
    {
        public string Make { get; set; }
        public string Model { get; set; }
        public string Vin { get; set; }
    }
}