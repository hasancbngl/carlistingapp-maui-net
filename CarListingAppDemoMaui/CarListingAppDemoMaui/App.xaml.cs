using CarListingAppDemoMaui.Repository;

namespace CarListingAppDemoMaui;

public partial class App : Application
{
    public static CarRepository CarRepository { get; private set; }
    public App(CarRepository carRepository)
    {
        InitializeComponent();
        MainPage = new AppShell();
        CarRepository = carRepository;
    }
}

