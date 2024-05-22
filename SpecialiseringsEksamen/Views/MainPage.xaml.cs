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

            var lockerId = LockerIdEntry.Text;

            if (string.IsNullOrWhiteSpace(lockerId))
            {
                await DisplayAlert("Error", "Please enter a valid Locker ID.", "OK");
                return;
            }

            var viewModel = BindingContext as MainViewModel;
            viewModel.IsBusy = true;

            await Task.Delay(2000); // Simulate an API call or hardware interaction.

            viewModel.IsBusy = false;

            await DisplayAlert("Success", $"Locker {lockerId} has been unlocked.", "OK");
        }
    }
}