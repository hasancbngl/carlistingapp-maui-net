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

        [ObservableProperty]
        bool isRefreshing;
        [ObservableProperty]
        string addEditButtonText;
        [ObservableProperty]
        Car carFromForm = new();

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
            if (string.IsNullOrEmpty(carFromForm.Model) || string.IsNullOrEmpty(carFromForm.Make) || string.IsNullOrEmpty(carFromForm.Vin)) return;
            if (carFromForm.Id == 0) await carApiService.AddCar(carFromForm);
            else await carApiService.UpdateCar(carFromForm);

            await ClearForm();
            await GetCarList();
        }

        [RelayCommand]
        async Task DeleteCar(int id)
        {
            if (id == 0) return;
            var found = Cars.FirstOrDefault(x => x.Id == id);
            Cars.Remove(found);
            carApiService.DeleteCar(id);
        }

        [RelayCommand]
        async Task UpdateCar(int id)
        {
            AddEditButtonText = editButtonText;
            carFromForm.Id = id;
            return;
        }

        [RelayCommand]
        async Task SetEditMode(int id)
        {
            AddEditButtonText = editButtonText;
            var car = Cars.FirstOrDefault(x => x.Id == id);
            carFromForm.Id = car.Id;
            carFromForm.Make = car.Make;
            carFromForm.Model = car.Model;
            carFromForm.Vin = car.Vin;
            OnPropertyChanged("CarFromForm");
        }

        [RelayCommand]
        async Task ClearForm()
        {
            AddEditButtonText = createButtonText;
            carFromForm.Make = "";
            carFromForm.Model = "";
            carFromForm.Vin = "";
            carFromForm.Id = 0;
            OnPropertyChanged("CarFromForm");
        }
    }
}

