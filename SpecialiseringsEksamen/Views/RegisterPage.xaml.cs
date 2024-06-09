using Microsoft.Maui.Controls;

namespace SpecialiseringsEksamen.Views
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            BindingContext = new ViewModels.RegisterViewModel();
        }
    }
}
