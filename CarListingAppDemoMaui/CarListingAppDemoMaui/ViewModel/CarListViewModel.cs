using System.Collections.ObjectModel;
using System.Diagnostics;
using CarListingAppDemoMaui.Model;
using CarListingAppDemoMaui.Repository;
using CarListingAppDemoMaui.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CarListingAppDemoMaui.ViewModel
{
    public partial class CarListViewModel : BaseViewModel
    {
        private readonly CarRepository repository;
        public ObservableCollection<Car> Cars { get; private set; } = new();
        [ObservableProperty]
        bool isRefreshing;

        public CarListViewModel(CarRepository repository)
        {
            Title = "Car List Screen";
            this.repository = repository;
        }

        [RelayCommand]
        async Task GetCarList()
        {
            if (IsLoading) return;
            try
            {
                IsLoading = true;
                if (Cars.Any()) Cars.Clear();
                var cars = repository.GetCars();
                foreach (var car in cars) Cars.Add(car);

            }
            catch (Exception e)
            {
                Debug.WriteLine($"Unable to get cars: {e.Message}");
                await Shell.Current.DisplayAlert("Error", "Failed to retrive list of cars.", "Ok");
            }
            finally
            {
                IsLoading = false;
                IsRefreshing = false;
            }
        }

        [RelayCommand]
        async Task GetCarDetails(Car car)
        {
            if (car == null) return;
            System.Console.WriteLine("hasaaan" + car.Model);
            await Shell.Current.GoToAsync(nameof(CarDetailsPage), true, new Dictionary<string, object>
            {
                { nameof(Car), car }
            });
        }
    }
}

