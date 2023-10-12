using CarListingAppDemoMaui.ViewModel;

namespace CarListingAppDemoMaui.View;

public partial class MainPage : ContentPage
{
    public MainPage(CarListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        viewModel.GetCarList();
    }
}


