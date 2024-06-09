using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using System.Threading.Tasks;
using SpecialiseringsEksamen.Services;

namespace SpecialiseringsEksamen.ViewModels
{
    public class RegisterViewModel : INotifyPropertyChanged
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

        public ICommand RegisterCommand { get; }
        public ICommand BackCommand { get; }

        public RegisterViewModel()
        {
            redisService = new RedisService();
            RegisterCommand = new Command(OnRegister);
            BackCommand = new Command(OnBack);
        }

        private async void OnRegister()
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

            // Attempt to register the user
            bool isRegistered = await redisService.RegisterUserAsync(Username, Password);

            IsBusy = false;

            if (isRegistered)
            {
                await Application.Current.MainPage.DisplayAlert("Success", "User registered successfully.", "OK");
                await Shell.Current.GoToAsync("//SignInPage");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "User already exists. Please choose a different username.", "OK");
            }
        }

        private async void OnBack()
        {
            await Shell.Current.GoToAsync("//SignInPage");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
