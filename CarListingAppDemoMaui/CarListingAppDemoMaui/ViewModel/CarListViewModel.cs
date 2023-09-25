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
        public ObservableCollection<Car> Cars { get; private set; } = new();
        const string editButtonText = "Update Car";
        const string createButtonText = "Add Car";
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

        public CarListViewModel()
        {
            Title = "Car List Screen";
            AddEditButtonText = createButtonText;
            GetCarList().Wait();
        }

        [RelayCommand]
        async Task GetCarList()
        {
            if (IsLoading) return;
            try
            {
                IsLoading = true;
                if (Cars.Any()) Cars.Clear();
                var cars = App.CarRepository.GetCars();
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
        async Task GetCarDetails(int id)
        {
            if (id == 0) return;
            await Shell.Current.GoToAsync($"{nameof(CarDetailsPage)}?Id={id}", true);
        }

        [RelayCommand]
        async Task SaveCar()
        {
            if (string.IsNullOrEmpty(Make) || string.IsNullOrEmpty(Model) || string.IsNullOrEmpty(vin))
            {
                await Shell.Current.DisplayAlert("Invalid Data", "Please insert valid data", "Ok");
                return;
            }

            var car = new Car
            {
                Make = Make,
                Model = Model,
                Vin = Vin
            };
            if (CarId != 0)
            {
                car.Id = CarId;
                App.CarRepository.UpdateCar(car);
                await Shell.Current.DisplayAlert("Info", App.CarRepository.StatusMessage, "Ok");
            }
            else
            {
                App.CarRepository.AddCar(car);
                await Shell.Current.DisplayAlert("Info", App.CarRepository.StatusMessage, "Ok");
            }

            await GetCarList();
            await ClearForm();
        }

        [RelayCommand]
        async Task DeleteCar(int id)
        {
            if (id == 0)
            {
                await Shell.Current.DisplayAlert("Invalid Record", "Please try again", "Ok");
                return;
            }
            App.CarRepository.DeleteCar(id);
            await Shell.Current.DisplayAlert("Deletion Successful", "Record Removed Successfully", "Ok");
            await GetCarList();

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
            var car = App.CarRepository.GetCar(id);
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
    }
}

