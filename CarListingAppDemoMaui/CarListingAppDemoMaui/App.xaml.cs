using CarListingAppDemoMaui.Service;

namespace CarListingAppDemoMaui;

public partial class App : Application
{
    public static CarDbService CarDbService { get; private set; }
    public App(CarDbService carDbService)
    {
        InitializeComponent();
        MainPage = new AppShell();
        CarDbService = carDbService;
    }
}

