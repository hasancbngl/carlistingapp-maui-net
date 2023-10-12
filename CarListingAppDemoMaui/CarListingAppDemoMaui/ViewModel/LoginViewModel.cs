using CarListingAppDemoMaui.Model;
using CarListingAppDemoMaui.Service;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CarListingAppDemoMaui.View;
using System.IdentityModel.Tokens.Jwt;

namespace CarListingAppDemoMaui.ViewModel
{
    public partial class LoginViewModel : BaseViewModel
    {
        [ObservableProperty]
        string username;
        [ObservableProperty]
        string password;
        [ObservableProperty]
        string errorMsg;
        [ObservableProperty]
        bool isErrorMsgVisible = false;
        private readonly CarApiService carApiService;

        public LoginViewModel(CarApiService carApiService)
        {
            this.carApiService = carApiService;
        }

        [RelayCommand]
        async Task Login()
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                DisplayErrorMsg();
            }
            else
            {
                var user = new LoginData
                {
                    Username = Username,
                    Password = Password
                };
                var response = await carApiService.Login(user);
                if (!string.IsNullOrEmpty(response.Token))
                {
                    // Store token in secure storage 
                    await SecureStorage.SetAsync("Token", response.Token);
                    var jsonToken = new JwtSecurityTokenHandler().ReadToken(response.Token) as JwtSecurityToken;

                  //  var role = jsonToken.Claims.FirstOrDefault(q => q.Type.Equals(ClaimTypes.Role))?.Value;
                    // navigate to app's main page
                  //  MenuBuilder.BuildMenu();
                    //  App.User = 
                    await Shell.Current.GoToAsync($"{nameof(MainPage)}");
                }
               else  DisplayErrorMsg();
            }
        }

        [RelayCommand]
        async Task Register()
        {
            await Shell.Current.GoToAsync($"{nameof(RegisterPage)}");
        }

        void DisplayErrorMsg()
        {
            errorMsg = "Invalid Login Attempt, check your email or password";
            isErrorMsgVisible = true;
            password = "";
        }
    }
}

