using System;
using CarListingAppDemoMaui.View;
using System.IdentityModel.Tokens.Jwt;

namespace CarListingAppDemoMaui.ViewModel
{
    public class LoadingViewModel : BaseViewModel
    {
        public LoadingViewModel()
        {
            CheckUserLoginDetails();
        }

        private async void CheckUserLoginDetails()
        {
            //retrieve token from storage
            var token = await SecureStorage.GetAsync("Token");
            if (string.IsNullOrEmpty(token)) await GoToLoginPage();
            else
            {
                // evaluate if token is valid
                var jsonToken = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;
                if (jsonToken.ValidTo < DateTime.UtcNow)
                {
                    SecureStorage.Remove("Token");
                    await GoToLoginPage();
                }
                else
                {
                    //var role = jsonToken.Claims.FirstOrDefault(q => q.Type.Equals(ClaimTypes.Role))?.Value;
                    //App.User = new User()
                    //{
                    //    Username = jsonToken.Claims.FirstOrDefault(q => q.Type.Equals(ClaimTypes.Email))?.Value,
                    //    Role = role
                    //};
                    //  MenuBuilder.BuildMenu();
                    await GoToMainPage();
                }
            }
        }

        private async Task GoToLoginPage()
        {
            await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
        }

        private async Task GoToMainPage()
        {
            await Shell.Current.GoToAsync($"{nameof(MainPage)}");
        }
    }
}

