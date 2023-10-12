using CarListingAppDemoMaui.ViewModel;

namespace CarListingAppDemoMaui.View;

public partial class LoadingPage : ContentPage
{
    public LoadingPage(LoadingViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
    }
}
