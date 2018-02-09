using Xamarin.Forms;

namespace App2
{
    /// <summary>
    /// MainPage
    /// </summary>
    public partial class MainPage : ContentPage
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MainPage()
        {
            InitializeComponent();

            BindingContext = new BarcodeViewModel();
        }
    }
}
