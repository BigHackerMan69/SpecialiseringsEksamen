using Microsoft.Maui.Controls;
using SpecialiseringsEksamen.ViewModels;

namespace SpecialiseringsEksamen.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }

        private async void UnlockLockerButton_Clicked(object sender, EventArgs e)
        {
            double secondsToVibrate = 0.1;
            TimeSpan vibrationLength = TimeSpan.FromSeconds(secondsToVibrate);
            Vibration.Default.Vibrate(vibrationLength);


            var viewModel = BindingContext as MainViewModel;
            viewModel.IsBusy = true;

            await Task.Delay(2000); // Simulate an API call or hardware interaction.

            viewModel.IsBusy = false;

            await DisplayAlert("Success", $"Your locker has been unlocked.", "OK");
        }
    }
}