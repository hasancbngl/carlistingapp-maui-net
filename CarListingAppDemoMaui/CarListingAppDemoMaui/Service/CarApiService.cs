using System.Net.Http.Json;
using CarListingAppDemoMaui.Model;
using Newtonsoft.Json;

namespace CarListingAppDemoMaui.Service
{
    public class CarApiService
    {
        HttpClient _httpClient;
        public string StatusMessage;
        public static string BaseAddress = "http://172.16.12.81:8080/api/car/";

        public CarApiService()
        {
            _httpClient = new() { BaseAddress = new Uri(BaseAddress) };
            _httpClient.Timeout = TimeSpan.FromSeconds(500);
        }

        public async Task<List<Car>> GetCars()
        {
            try
            {
              //  await SetAuthToken();
                Console.WriteLine("get carrs");
                var apiResponse = await _httpClient.GetStringAsync("");
                Console.WriteLine("apiresponse:" + apiResponse);
                var carResponse = JsonConvert.DeserializeObject<CarResponse>(apiResponse);
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
                Console.WriteLine("car response: " + carResponse);
                return carResponse.data[0];
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to retrieve data.";
                return null;
            }
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
                return null;
            }
        }

        public async Task UpdateCar(Car car)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("", car);
            //    response.EnsureSuccessStatusCode();
                //  var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(response);
                //  var carResponse = JsonConvert.DeserializeObject<CarResponse>(content);
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to update data.";
            }
        }

        public async void DeleteCar(int id)
        {
            try
            {
                Console.WriteLine("deletee:" + id);
                var response = await _httpClient.DeleteAsync($"{id}");
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine("delete: " + response);
                var carResponse = JsonConvert.DeserializeObject<CarResponse>(content);
                Console.WriteLine("delete response:" + carResponse);
                StatusMessage = "Delete Successful";

            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to delete data.";
            }
        }

        public async Task<LoginResponseDto> Login(LoginData userData)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("login", userData);
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine("loginResponse: " + response);
                var loginResponse = JsonConvert.DeserializeObject<LoginResponseDto>(content);
                Console.WriteLine("loginResponse:" + loginResponse.data);
                return loginResponse;
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to get auth response data.";
                return null;
            }
        }

        public async Task<RegisterResponse> Register(RegisterDto userData)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("register", userData);
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine("registerresponse: " + response);
                var registerResponse = JsonConvert.DeserializeObject<RegisterResponse>(content);
                Console.WriteLine("registerresponse:" + response);
                return registerResponse;
            }
            catch (Exception ex)
            {
                StatusMessage = "Failed to get auth response data.";
                return null;
            }
        }

        public async Task SetAuthToken()
        {
            var token = await SecureStorage.GetAsync("Token");
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }
    }
}

