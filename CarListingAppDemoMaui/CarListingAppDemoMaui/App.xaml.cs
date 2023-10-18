using System.IdentityModel.Tokens.Jwt;
using CarListingAppDemoMaui.Model;
using CarListingAppDemoMaui.Service;
using CarListingAppDemoMaui.View;

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

