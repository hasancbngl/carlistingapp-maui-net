using CarListingAppDemoMaui.Service;
using CarListingAppDemoMaui.View;
using CarListingAppDemoMaui.ViewModel;
using Microsoft.Extensions.Logging;

namespace CarListingAppDemoMaui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "carsDb.db3");
        builder.Services.AddSingleton(repo => ActivatorUtilities.CreateInstance<CarDbService>(repo, dbPath));
        builder.Services.AddTransient<CarApiService>();
        builder.Services.AddSingleton<CarListViewModel>();
        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddSingleton<LoadingPageViewModel>();
        //new instance every time
        builder.Services.AddTransient<CarDetailsViewModel>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<LoadingPage>();
        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddTransient<CarDetailsPage>();
        return builder.Build();
    }
}

