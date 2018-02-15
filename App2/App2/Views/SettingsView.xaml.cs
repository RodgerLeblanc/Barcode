using App2.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsView : ContentPage
    {
        public SettingsView(ScanningOptionsViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = viewModel;
        }
    }
}