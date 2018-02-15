using App2.Helpers;
using App2.Views;
using System.Windows.Input;

namespace App2
{
    /// <summary>
    /// BarcodeViewModel
    /// basic ViewModel without underlying model to demonstrate different Barcode implementations.
    /// </summary>
    public class BarcodeViewModel : BaseViewModel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public BarcodeViewModel()
        {
            ZXing = new ZXingViewModel();

            OpenSettingsCommand = CommandHelper.CreateAsyncCommand(async () =>
            {
                SettingsView settings = new SettingsView(ZXing.ScanningOptions);
                await NavigationHelper.PushAsync(settings);
            });
        }

        /// <summary>
        /// ZXing
        /// </summary>
        public ZXingViewModel ZXing { get; set; }

        /// <summary>
        /// OpenSettingsCommand
        /// </summary>
        public ICommand OpenSettingsCommand { get; }
    }
}