using Microsoft.Maui.Controls;
using SpecialiseringsEksamen.ViewModels;
using SpecialiseringsEksamen.Services;

namespace SpecialiseringsEksamen.Views
{
    public partial class SignInPage : ContentPage
    {
        public SignInPage(LoginViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
