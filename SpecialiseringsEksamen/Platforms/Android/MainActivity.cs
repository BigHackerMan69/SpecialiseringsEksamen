using Android.App;
using Android.Content.PM;
using Android.OS;

namespace SpecialiseringsEksamen;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
public class MainActivity : MauiAppCompatActivity
{

    protected override void OnCreate(Android.OS.Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        //System.Net.ServicePointManager.ServerCertificateValidationCallback += (sender, cart, chain, sslPolicyErrors) => true;
    }
}
