using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CarListingAppDemoMaui.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        string _title;
        bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                if (_isBusy == value) return;
                _isBusy = value;
                OnPropertyChanged();
            }
        }
        public string Title
        {
            get => _title; 
            set
            {
                if (_title == value) return;
                _title = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

