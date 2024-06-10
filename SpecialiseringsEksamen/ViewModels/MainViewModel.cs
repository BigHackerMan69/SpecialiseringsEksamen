using System;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Devices;
using SpecialiseringsEksamen.Services;
using System.Threading.Tasks;

namespace SpecialiseringsEksamen.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private bool isBusy;
        private string lockerNumber;
        private readonly ApiService _apiService;

        public MainViewModel(ApiService apiService)
        {
            _apiService = apiService;
            UnlockCommand = new Command(async () => await OnUnlock());
            LockCommand = new Command(async () => await OnLock());
            LogoutCommand = new Command(async () => await OnLogout());
        }

        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }

        public string LockerNumber
        {
            get => lockerNumber;
            set => SetProperty(ref lockerNumber, value);
        }

        public ICommand UnlockCommand { get; }
        public ICommand LockCommand { get; }
        public ICommand LogoutCommand { get; }

        private async Task OnUnlock()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var response = await _apiService.UnlockLockerAsync(LockerNumber);

                if (response.isSuccess)
                {
                    HapticFeedback.Perform(HapticFeedbackType.Click);
                    await Application.Current.MainPage.DisplayAlert("Success", "Locker unlocked successfully.", "OK");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", response.message, "OK");
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

        private async Task OnLock()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var response = await _apiService.LockLockerAsync(LockerNumber);

                if (response.isSuccess)
                {
                    HapticFeedback.Perform(HapticFeedbackType.Click);
                    await Application.Current.MainPage.DisplayAlert("Success", "Locker locked successfully.", "OK");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", response.message, "OK");
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
