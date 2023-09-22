using CarListingAppDemoMaui.Model;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CarListingAppDemoMaui.ViewModel
{
    [QueryProperty(nameof(Car), "Car")]
    public partial class CarDetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        Car car;
    }
}
