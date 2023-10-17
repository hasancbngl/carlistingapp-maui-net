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

    protected override bool OnBackButtonPressed()
    {
        Application.Current.Quit();
        return true;
    }
}


