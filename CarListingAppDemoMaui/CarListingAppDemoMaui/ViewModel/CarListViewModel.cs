using System.Collections.ObjectModel;
using System.Diagnostics;
using CarListingAppDemoMaui.Model;
using CarListingAppDemoMaui.Repository;
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
        async Task GetCarListAsync()
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
    }
}

