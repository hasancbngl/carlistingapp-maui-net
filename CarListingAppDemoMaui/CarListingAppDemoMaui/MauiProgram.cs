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
        builder.Services.AddSingleton<CarApiService>();
        builder.Services.AddSingleton<CarListViewModel>();
        builder.Services.AddSingleton<LoginViewModel>();
        //new instance every time
        builder.Services.AddSingleton<CarDetailsViewModel>();
        builder.Services.AddSingleton<RegisterViewModel>();
        builder.Services.AddSingleton<LoginViewModel>();
        builder.Services.AddSingleton<LoadingViewModel>();
        builder.Services.AddSingleton<LoadingPage>();
        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<LoginPage>();
        builder.Services.AddSingleton<CarDetailsPage>();
        builder.Services.AddSingleton<RegisterPage>();
        return builder.Build();
    }
}

