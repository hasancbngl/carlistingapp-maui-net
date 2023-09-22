using SQLite;

namespace CarListingAppDemoMaui.Model
{
    public abstract class BaseEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}