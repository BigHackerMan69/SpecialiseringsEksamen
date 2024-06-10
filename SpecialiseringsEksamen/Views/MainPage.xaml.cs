using Microsoft.Maui.Controls;
using SpecialiseringsEksamen.ViewModels;
using SpecialiseringsEksamen.Services;

namespace SpecialiseringsEksamen.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}
