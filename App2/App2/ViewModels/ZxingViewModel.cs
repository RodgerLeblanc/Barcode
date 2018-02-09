using App2.Helpers;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace App2
{
    /// <summary>
    /// ZXingViewModel
    /// </summary>
    public class ZXingViewModel : BaseViewModel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ZXingViewModel()
        {
            InitializeCommands();
        }

        #region Properties
        /// <summary>
        /// IsScanning backend property.
        /// </summary>
        private bool _isScanning;
        /// <summary>
        /// IsScanning
        /// </summary>
        public bool IsScanning
        {
            get { return _isScanning; }
            set
            {
                SetProperty(ref _isScanning, value);
                ((Command)TestZXingCommand).ChangeCanExecute();
            }
        }

        /// <summary>
        /// Barcode backend property.
        /// </summary>
        private string _barcode = "No barcode yet...";
        /// <summary>
        /// Barcode
        /// </summary>
        public string Barcode
        {
            get { return _barcode; }
            set { SetProperty(ref _barcode, value); }
        }
        #endregion

        #region Commands
        /// <summary>
        /// TestZXingCommand
        /// </summary>
        public ICommand TestZXingCommand { get; set; }

        /// <summary>
        /// TestZXingCommand execute.
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private async Task TestZXing(object arg)
        {
            MobileBarcodeScanningOptions options = new MobileBarcodeScanningOptions
            {
                TryHarder = true
            };

            ZXingScannerPage scanPage = new ZXingScannerPage(options);
            scanPage.Appearing += ScanPage_Appearing;
            scanPage.Disappearing += ScanPage_Disappearing;

            scanPage.OnScanResult += (result) =>
            {
                // Stop scanning
                scanPage.IsScanning = false;
                Barcode = result.Text;

                // Pop the page and show the result
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await NavigationHelper.PopAsync();
                    AlertHelper.DisplayAlert("Scanned Barcode", result.Text, "OK");
                });
            };

            // Navigate to our scanner page
            await NavigationHelper.PushAsync(scanPage);
        }

        /// <summary>
        /// TestZXingCommand canExecute.
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private bool CanTestZXing(object arg)
        {
            return !IsScanning;
        }
        #endregion

        /// <summary>
        /// Initialize commands.
        /// </summary>
        private void InitializeCommands()
        {
            TestZXingCommand = CommandHelper.CreateAsyncCommand(TestZXing, CanTestZXing);
        }

        /// <summary>
        /// ScanPage appearing event handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScanPage_Appearing(object sender, System.EventArgs e)
        {
            IsScanning = true;

            ZXingScannerPage scanPage = sender as ZXingScannerPage;
            OnScanStarted(scanPage);
        }

        /// <summary>
        /// ScanPage disappearing event handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScanPage_Disappearing(object sender, System.EventArgs e)
        {
            IsScanning = false;
        }

        /// <summary>
        /// OnScanStarted method to run platform specific code.
        /// </summary>
        /// <param name="scanPage"></param>
        protected virtual void OnScanStarted(ZXingScannerPage scanPage)
        {
            //Nothing here, see Android override.
        }
    }
}