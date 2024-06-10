using System.Windows.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;
using SpecialiseringsEksamen.Services;
using System.Threading.Tasks;

namespace SpecialiseringsEksamen.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private bool isBusy;
        private string username;
        private string password;
        private readonly ApiService _apiService;

        public LoginViewModel(ApiService apiService)
        {
            _apiService = apiService;
            LoginCommand = new Command(async () => await OnSignIn());
            NavigateToRegisterCommand = new Command(async () => await OnNavigateToRegister());
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

        public ICommand LoginCommand { get; }
        public ICommand NavigateToRegisterCommand { get; }

        private async Task OnNavigateToRegister()
        {
            await Shell.Current.GoToAsync("//RegisterPage");
        }

        private async Task OnSignIn()
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

            var (isSuccess, message) = await _apiService.LoginUserAsync(Username, Password);

            IsBusy = false;

            if (isSuccess)
            {
                await SecureStorage.SetAsync("username", Username);
                await SecureStorage.SetAsync("password", Password);

                await Shell.Current.GoToAsync("//MainPage");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", message, "OK");
            }
        }
    }
}
