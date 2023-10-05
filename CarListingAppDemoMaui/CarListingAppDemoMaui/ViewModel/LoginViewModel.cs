using System;
using AndroidX.AppCompat.View.Menu;
using System.Security.Claims;
using CarListingAppDemoMaui.Model;
using CarListingAppDemoMaui.Service;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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
                errorMsg = "Invalid Login Attempt";
                isErrorMsgVisible = true;
            }
            else
            {
                var loginModel = new LoginModel(Username, Password);
                var response = await carApiService.Login(loginModel);
                    if (!string.IsNullOrEmpty(response.Token))
                    {
                        // Store token in secure storage 
                        await SecureStorage.SetAsync("Token", response.Token);

                        // build a menu on the fly...based on the user role
                        var jsonToken = new JwtSecurityTokenHandler().ReadToken(response.Token) as JwtSecurityToken;

                        var role = jsonToken.Claims.FirstOrDefault(q => q.Type.Equals(ClaimTypes.Role))?.Value;

                        App.UserInfo = new UserInfo()
                        {
                            Username = Username,
                            Role = role
                        };
                    isErrorMsgVisible = false;

                        // navigate to app's main page
                        MenuBuilder.BuildMenu();
                        await Shell.Current.GoToAsync($"{nameof(MainPage)}");

                    }
                    else
                    {
                        errorMsg = "Invalid Login Attempt";
                    isErrorMsgVisible = true;
                    }
            }
        }
    }
}

