using System.Net.Http.Json;
using CarListingAppDemoMaui.Model;
using Newtonsoft.Json;

namespace CarListingAppDemoMaui.Service
{
    public class CarApiService
    {
        HttpClient _httpClient;
        public string StatusMessage;
        public static string BaseAddress = "http://192.168.0.215:8080/api/car";

        public CarApiService()
        {
            _httpClient = new() { BaseAddress = new Uri(BaseAddress) };
            _httpClient.Timeout = TimeSpan.FromSeconds(500);
        }

        public async Task<List<Car>> GetCars()
        {
            try
            {
                var response = await _httpClient.GetStringAsync("");
                System.Console.WriteLine("get cars response:" + response);
                return JsonConvert.DeserializeObject<List<Car>>(response);
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to retrieve data.";
            }

            return null;
        }

        public async Task<Car> GetCar(int id)
        {
            try
            {
                var response = await _httpClient.GetStringAsync("" + id);
                return JsonConvert.DeserializeObject<Car>(response);
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to retrieve data.";
            }

            return null;
        }

        public async Task AddCar(Car car)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("", car);
                Console.WriteLine(response);
                response.EnsureSuccessStatusCode();
                StatusMessage = "Insert Successful";
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                StatusMessage = "Failed to add data.";
            }
        }

        public async Task DeleteCar(int id)
        {
            try
            {

                var response = await _httpClient.DeleteAsync("/car/" + id);
                response.EnsureSuccessStatusCode();
                StatusMessage = "Delete Successful";
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to delete data.";
            }
        }

        public async Task UpdateCar(int id, Car car)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("/cars/" + id, car);
                response.EnsureSuccessStatusCode();
                StatusMessage = "Update Successful";
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to update data.";
            }
        }
    }
}

