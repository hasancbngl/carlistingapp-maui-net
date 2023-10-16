using CarListingAppDemoMaui.Model;
using CarListingAppDemoMaui.Service;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CarListingAppDemoMaui.View;
using System.IdentityModel.Tokens.Jwt;
using System.ComponentModel;

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
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                DisplayErrorMsg("please check your email or password");
            }
            else
            {
                var user = new LoginData
                {
                    Username = Username,
                    Password = Password
                };
                var response = await carApiService.Login(user);
                if (response.success)
                {
                    await SecureStorage.SetAsync("Token", response.data.Token);
                    var jsonToken = new JwtSecurityTokenHandler().ReadToken(response.data.Token) as JwtSecurityToken;
                    await Shell.Current.GoToAsync($"{nameof(MainPage)}");
                }
               else  DisplayErrorMsg(response.message);
            }
        }

        [RelayCommand]
        async Task Register()
        {
            await Shell.Current.GoToAsync($"{nameof(RegisterPage)}");
        }

        void DisplayErrorMsg(string customMsg)
        {
            ErrorMsg = customMsg;
            IsErrorMsgVisible = true;
            Password = "";
            OnPropertyChanged(new PropertyChangedEventArgs(null));
        }
    }
}

