using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using System.Threading.Tasks;
using SpecialiseringsEksamen.Services;

namespace SpecialiseringsEksamen.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private bool isBusy;
        private readonly RedisService redisService;

        public bool IsBusy
        {
            get => isBusy;
            set
            {
                if (isBusy != value)
                {
                    isBusy = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand UnlockCommand { get; }
        public ICommand LogoutCommand { get; }

        public MainViewModel()
        {
            UnlockCommand = new Command(OnUnlock);
            LogoutCommand = new Command(OnLogout);
        }

        private async void OnUnlock()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            // Simulate an API call to unlock the locker
            await Task.Delay(2000);

            // Here you would add your logic to unlock the locker
            bool isUnlocked = true; // Assume unlocking is successful

            IsBusy = false;

            if (isUnlocked)
            {
                await Application.Current.MainPage.DisplayAlert("Success", "Locker unlocked successfully.", "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to unlock the locker. Please try again.", "OK");
            }
        }

        private async void OnLogout()
        {
            // Clear credentials from secure storage
            SecureStorage.Remove("username");
            SecureStorage.Remove("password");

            // Navigate back to the SignInPage
            await Shell.Current.GoToAsync("//SignInPage");
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
