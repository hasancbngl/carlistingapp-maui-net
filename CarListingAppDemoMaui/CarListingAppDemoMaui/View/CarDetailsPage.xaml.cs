using CarListingAppDemoMaui.ViewModel;

namespace CarListingAppDemoMaui.View;

public partial class CarDetailsPage : ContentPage
{
    public CarDetailsPage(CarDetailsViewModel carDetailsViewModel)
    {
        InitializeComponent();
        BindingContext = carDetailsViewModel;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        System.Console.WriteLine("hasaaan" + args);
        base.OnNavigatedTo(args);
    }
}
