using System.Web;
using CarListingAppDemoMaui.Model;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CarListingAppDemoMaui.ViewModel
{
    [QueryProperty(nameof(Id), nameof(Id))]
    public partial class CarDetailsViewModel : BaseViewModel, IQueryAttributable
    {
        [ObservableProperty]
        Car car;
        [ObservableProperty]
        int id;

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Id = Convert.ToInt32(HttpUtility.UrlDecode((string)query["Id"]));
            Car = App.CarRepository.GetCar(Id);
        }
    }
}
