using CommunityToolkit.Mvvm.ComponentModel;

namespace CarListingAppDemoMaui.ViewModel
{
    public partial class BaseViewModel : ObservableObject
    {
        //observable object is replacement of INotifyProperty changes using CommunityToolkit.Mvvm dependency
        //observableproperty will generate required getter setter and public accessable value.
        [ObservableProperty]
        string title;
        [ObservableProperty]
        bool isLoading;

        public bool IsNotLoading => !IsLoading;
    }
}

