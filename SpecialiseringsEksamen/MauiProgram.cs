using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
using SpecialiseringsEksamen.Services;
using SpecialiseringsEksamen.ViewModels;
using SpecialiseringsEksamen.Views;

namespace SpecialiseringsEksamen
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<ApiService>();

           
            builder.Services.AddTransient<MainViewModel>();
            builder.Services.AddTransient<RegisterViewModel>();
            builder.Services.AddTransient<LoginViewModel>();

     
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<SignInPage>();
            builder.Services.AddTransient<RegisterPage>();

            return builder.Build();
        }
    }
}
