using System;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using SpecialiseringsEksamen.Services;
using System.Threading.Tasks;

namespace SpecialiseringsEksamen.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private bool isBusy;
        private readonly ApiService _apiService;

        public MainViewModel(ApiService apiService)
        {
            _apiService = apiService;
            UnlockCommand = new Command(async () => await OnUnlock());
            LogoutCommand = new Command(async () => await OnLogout());
        }

        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }

        public ICommand UnlockCommand { get; }
        public ICommand LogoutCommand { get; }

        private async Task OnUnlock()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var response = await _apiService.UnlockLockerAsync("lockerNumber"); 

               
                if (response.isSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("Success", "Locker unlocked successfully.", "OK");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", response.message, "Failed to unlock the locker. Please try again.");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task OnLogout()
        {
            SecureStorage.Remove("username");
            SecureStorage.Remove("password");

            await Shell.Current.GoToAsync("//SignInPage");
        }
    }
}
