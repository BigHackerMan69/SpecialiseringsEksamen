using System.Windows.Input;
using Microsoft.Maui.Controls;
using System.Threading.Tasks;
using System;
using Microsoft.Maui.Storage;
using SpecialiseringsEksamen.Services;

namespace SpecialiseringsEksamen.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private bool isBusy;
        private string username;
        private string password;
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

        public string Username
        {
            get => username;
            set
            {
                if (username != value)
                {
                    username = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Password
        {
            get => password;
            set
            {
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand LoginCommand { get; }
        public ICommand NavigateToRegisterCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnSignIn);
            redisService = new RedisService();
            NavigateToRegisterCommand = new Command(OnNavigateToRegister);
        }
        private async void OnNavigateToRegister()
        {
            await Shell.Current.GoToAsync("//RegisterPage");
        }

        private async void OnSignIn()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please enter both username and password.", "OK");
                IsBusy = false;
                return;
            }

            // Attempt to log in the user
            bool isSignedIn = await redisService.LoginUserAsync(Username, Password);

            IsBusy = false;

            if (isSignedIn)
            {
                // Save credentials in secure storage
                await SecureStorage.SetAsync("username", Username);
                await SecureStorage.SetAsync("password", Password);

                // Navigate to MainPage
                await Shell.Current.GoToAsync("//MainPage");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Invalid username or password. Please try again.", "OK");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
