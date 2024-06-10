using System.Windows.Input;
using Microsoft.Maui.Controls;
using System.Threading.Tasks;
using SpecialiseringsEksamen.Services;

namespace SpecialiseringsEksamen.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private bool isBusy;
        private string username;
        private string password;
        private readonly ApiService _apiService;

        public RegisterViewModel(ApiService apiService)
        {
            _apiService = apiService;
            RegisterCommand = new Command(async () => await OnRegister());
            BackCommand = new Command(async () => await OnBack());
        }

        public bool IsBusy
        {
            get => isBusy;
            set => SetProperty(ref isBusy, value);
        }

        public string Username
        {
            get => username;
            set => SetProperty(ref username, value);
        }

        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        public ICommand RegisterCommand { get; }
        public ICommand BackCommand { get; }

        private async Task OnRegister()
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

            var (isSuccess, message) = await _apiService.RegisterUserAsync(Username, Password);

            IsBusy = false;

            if (isSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Success", "User registered successfully.", "OK");
                await Shell.Current.GoToAsync("//SignInPage");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", message, "OK");
            }
        }

        private async Task OnBack()
        {
            await Shell.Current.GoToAsync("//SignInPage");
        }
    }
}
