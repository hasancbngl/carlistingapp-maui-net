using System.Collections.ObjectModel;
using System.Diagnostics;
using CarListingAppDemoMaui.Model;
using CarListingAppDemoMaui.Service;
using CarListingAppDemoMaui.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CarListingAppDemoMaui.ViewModel
{
    public partial class CarListViewModel : BaseViewModel
    {
        public ObservableCollection<Car> Cars { get; private set; } = new();
        const string editButtonText = "Update Car";
        const string createButtonText = "Add Car";
        private readonly CarApiService carApiService;
        NetworkAccess accessType = Connectivity.Current.NetworkAccess;
        string message = string.Empty;

        [ObservableProperty]
        bool isRefreshing;
        [ObservableProperty]
        string make;
        [ObservableProperty]
        string model;
        [ObservableProperty]
        string vin;
        [ObservableProperty]
        string addEditButtonText;
        [ObservableProperty]
        int carId;

        public CarListViewModel(CarApiService carApiService)
        {
            this.carApiService = carApiService;
            Title = "Car List Screen";
            AddEditButtonText = createButtonText;
        }

        public async Task GetCarList()
        {
            if (IsLoading) return;
            try
            {
                IsLoading = true;
                if (Cars.Count > 0) Cars.Clear();
                var cars = new List<Car>();
                if (accessType == NetworkAccess.Internet)
                {
                    cars = await carApiService.GetCars();
                    System.Console.WriteLine(cars);
                    if (cars.Count > 0)
                    {
                        App.CarDbService.ClearCars();
                        App.CarDbService.AddCars(cars);
                    }
                }
                else
                {
                    cars = App.CarDbService.GetCars();
                }
                foreach (var car in cars)
                {
                    Console.WriteLine(car);
                    Cars.Add(car);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get cars: {ex.Message}");
                await ShowAlert("Failed to retrive list of cars.");
            }
            finally
            {
                IsLoading = false;
                IsRefreshing = false;
            }
        }

        [RelayCommand]
        async Task GetCarDetails(int id)
        {
            if (id == 0) return;
            await Shell.Current.GoToAsync($"{nameof(CarDetailsPage)}?Id={id}", true);
        }

        [RelayCommand]
        async Task SaveCar()
        {
            if (string.IsNullOrEmpty(Make) || string.IsNullOrEmpty(Model) || string.IsNullOrEmpty(Vin))
            {
                await ShowAlert("Please insert valid data");
                return;
            }

            var car = new Car
            {
                Id = CarId,
                Make = Make,
                Model = Model,
                Vin = Vin
            };

            if (CarId != 0)
            {
                await carApiService.UpdateCar(CarId, car);
                message = carApiService.StatusMessage;
            }
            else
            {
                await carApiService.AddCar(car);
                message = carApiService.StatusMessage;
            }
            await ShowAlert(message);
            await GetCarList();
            await ClearForm();
            await GetCarList();
        }

        [RelayCommand]
        async Task DeleteCar(int id)
        {
            if (id == 0)
            {
                await ShowAlert("Please try again");
                return;
            }
            Cars.RemoveAt(id);
            await carApiService.DeleteCar(id);
            message = carApiService.StatusMessage;
            await ShowAlert(message);
        }

        [RelayCommand]
        async Task UpdateCar(int id)
        {
            AddEditButtonText = editButtonText;
            return;
        }

        [RelayCommand]
        async Task SetEditMode(int id)
        {
            AddEditButtonText = editButtonText;
            CarId = id;
            var car = App.CarDbService.GetCar(id);
            Make = car.Make;
            Model = car.Model;
            Vin = car.Vin;
        }

        [RelayCommand]
        async Task ClearForm()
        {
            AddEditButtonText = createButtonText;
            CarId = 0;
            Make = string.Empty;
            Model = string.Empty;
            Vin = string.Empty;
        }

        private async Task ShowAlert(string message)
        {
            await Shell.Current.DisplayAlert("Info", message, "Ok");
        }
    }
}

