namespace SpecialiseringsEksamen;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();

		Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
		Routing.RegisterRoute(nameof(SignInPage), typeof(SignInPage));
		Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));

		Shell.Current.GoToAsync("//LoginPage");

        CheckLoginState();


	}
    private async void CheckLoginState()
    {
        var username = await SecureStorage.GetAsync("username");
        var password = await SecureStorage.GetAsync("password");

        if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
        {
            // Navigate to MainPage if credentials are found
            await Shell.Current.GoToAsync("//MainPage");
        }
        else
        {
            // Navigate to SignInPage if no credentials are found
            await Shell.Current.GoToAsync("//SignInPage");
        }
    }
}
