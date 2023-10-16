using System;
using System.ComponentModel;
using CarListingAppDemoMaui.Model;
using CarListingAppDemoMaui.Service;
using CarListingAppDemoMaui.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CarListingAppDemoMaui.ViewModel
{
    public partial class RegisterViewModel :BaseViewModel
    {
        [ObservableProperty]
        string username;
        [ObservableProperty]
        string password;
        [ObservableProperty]
        string name;
        [ObservableProperty]
        string surname;
        [ObservableProperty]
        string phoneNumber;
        [ObservableProperty]
        string email;
        [ObservableProperty]
        string errorMsg;
        [ObservableProperty]
        bool isErrorMsgVisible = false;
        private readonly CarApiService carApiService;

        public RegisterViewModel(CarApiService carApiService)
        {
            this.carApiService = carApiService;
        }

        [RelayCommand]
        async Task Register()
        {
            if (IsEmpty(Name) || IsEmpty(Username) || IsEmpty(PhoneNumber) || IsEmpty(Password)
                || IsEmpty(Email) || IsEmpty(Surname))
            {
                DisplayErrorMsg("All fields are required.Please fill in.");
            } else
            {
                var registerDto = new RegisterDto
                {
                    Name = Name,
                    Username = Username,
                    Password = Password,
                    Email = Email,
                    Surname = Surname,
                    PhoneNumber = PhoneNumber
                };
                var response = await carApiService.Register(registerDto);
                if (response.data)
                {
                    await Shell.Current.GoToAsync($"{nameof(MainPage)}");
                }
                else DisplayErrorMsg("An error occured.Pleae check your network connection");
            }
        }

        void DisplayErrorMsg(string customMsg)
        {
            ErrorMsg = customMsg;
            IsErrorMsgVisible = true;
            Password = "";
            OnPropertyChanged(new PropertyChangedEventArgs(null));
        }

        bool IsEmpty(string x)
        {
            return string.IsNullOrWhiteSpace(x);
        }
    }
}

