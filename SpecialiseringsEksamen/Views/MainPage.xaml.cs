using Microsoft.Maui.Controls;
using SpecialiseringsEksamen.ViewModels;
using SpecialiseringsEksamen.Services;
using System;

namespace SpecialiseringsEksamen.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly ApiService _apiService;

        public MainPage()
        {
            InitializeComponent();
            _apiService = DependencyService.Get<ApiService>();
            BindingContext = new MainViewModel(_apiService);
        }

        private async void UnlockLockerButton_Clicked(object sender, EventArgs e)
        {
            double secondsToVibrate = 0.1;
            TimeSpan vibrationLength = TimeSpan.FromSeconds(secondsToVibrate);
            Vibration.Default.Vibrate(vibrationLength);

            var viewModel = BindingContext as MainViewModel;
            if (viewModel != null)
            {
                viewModel.IsBusy = true;

                var (isSuccess, message) = await _apiService.UnlockLockerAsync("lockerNumber"); 

                viewModel.IsBusy = false;

                if (isSuccess)
                {
                    await DisplayAlert("Success", "Your locker has been unlocked.", "OK");
                }
                else
                {
                    await DisplayAlert("Error", message, "OK");
                }
            }
        }
    }
}
