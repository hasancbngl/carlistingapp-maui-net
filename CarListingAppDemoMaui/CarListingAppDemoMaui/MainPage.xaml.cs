using CarListingAppDemoMaui.ViewModel;

namespace CarListingAppDemoMaui;

public partial class MainPage : ContentPage
{
    public MainPage(CarListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}


