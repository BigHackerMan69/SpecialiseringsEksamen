using Microsoft.Maui.Controls;

namespace SpecialiseringsEksamen.Views
{
    public partial class SignInPage : ContentPage
    {
        public SignInPage()
        {
            InitializeComponent();
            BindingContext = new ViewModels.LoginViewModel();
        }
    }
}
