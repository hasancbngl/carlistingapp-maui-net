using CarListingAppDemoMaui.ViewModel;

namespace CarListingAppDemoMaui.View;

public partial class LoadingPage : ContentPage
{
    public LoadingPage(LoadingPageViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
    }
}
