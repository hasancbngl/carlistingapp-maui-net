using CarListingAppDemoMaui.ViewModel;

namespace CarListingAppDemoMaui.View;

public partial class RegisterPage : ContentPage
{
    public RegisterPage(RegisterViewModel viewmodel)
    {
        InitializeComponent();
        this.BindingContext = viewmodel;
    }
}
