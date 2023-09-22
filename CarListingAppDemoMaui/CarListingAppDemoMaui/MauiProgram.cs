using CarListingAppDemoMaui.Repository;
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
        builder.Services.AddSingleton(repo => ActivatorUtilities.CreateInstance<CarRepository>(repo, dbPath));
        builder.Services.AddSingleton<CarListViewModel>();
        //new instance every time
        builder.Services.AddTransient<CarDetailsViewModel>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<CarDetailsPage>();
        return builder.Build();
    }
}

