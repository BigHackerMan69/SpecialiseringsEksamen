using SpecialiseringsEksamen.ViewModels;
using Microsoft.Maui.Controls;

namespace SpecialiseringsEksamen.Views
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage(RegisterViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
