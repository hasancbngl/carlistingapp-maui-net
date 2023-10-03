using System.Net.Http;
using System.Net.Http.Json;
using Android.Media.TV;
using CarListingAppDemoMaui.Model;
using Newtonsoft.Json;
using Org.Apache.Http.Protocol;

namespace CarListingAppDemoMaui.Service
{
    public class CarApiService
    {
        HttpClient _httpClient;
        public string StatusMessage;
        public static string BaseAddress = "http://192.168.0.215:8080/api/car/";

        public CarApiService()
        {
            _httpClient = new() { BaseAddress = new Uri(BaseAddress) };
            _httpClient.Timeout = TimeSpan.FromSeconds(500);
        }

        public async Task<List<Car>> GetCars()
        {
            try
            {
                Console.WriteLine("get carrs");
                var apiResponse = await _httpClient.GetStringAsync("");
                Console.WriteLine( "apiresponse:" + apiResponse);
                var carResponse =  JsonConvert.DeserializeObject<CarResponse>(apiResponse);
                Console.WriteLine("carresponse:" + carResponse.data);
          
                return carResponse.data;
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to retrieve data.";
                Console.WriteLine("error getting cars:" + ex.Message);
            }

            return null;
        }

        public async Task<Car> GetCar(int id)
        {
            try
            {
                var response = await _httpClient.GetStringAsync($"{id}");
                var carResponse = JsonConvert.DeserializeObject<CarResponse>(response);
                return carResponse.data[0];
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to retrieve data.";
            }

            return null;
        }

        public async Task<List<Car>> AddCar(Car car)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("", car);
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(response);
                StatusMessage = "Insert Successful";
                var carResponse = JsonConvert.DeserializeObject<CarResponse>(content);
                Console.WriteLine("carresponse:" + carResponse);
                return carResponse.data;
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                StatusMessage = "Failed to add data.";
            }
            return null;
        }

        public async Task<List<Car>> DeleteCar(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{id}");
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine("delete: "+response);
                var carResponse = JsonConvert.DeserializeObject<CarResponse>(content);
                Console.WriteLine("delete response:" + carResponse);
                StatusMessage = "Delete Successful";
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to delete data.";
            }
            return null;
        }

        public async Task UpdateCar(int id, Car car)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{id}", car);
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

