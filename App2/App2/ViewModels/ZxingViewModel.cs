using App2.Controls;
using App2.Helpers;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing.Mobile;

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
        private string _barcode = null;
        /// <summary>
        /// Barcode
        /// </summary>
        public string Barcode
        {
            get { return _barcode; }
            set { SetProperty(ref _barcode, value); }
        }

        /// <summary>
        /// TimeToScan backend property.
        /// </summary>
        private TimeSpan? _timeToScan;
        /// <summary>
        /// TimeToScan
        /// </summary>
        public TimeSpan? TimeToScan
        {
            get { return _timeToScan; }
            set { SetProperty(ref _timeToScan, value); }
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
            DateTime creationTime = DateTime.Now;

            MobileBarcodeScanningOptions options = new MobileBarcodeScanningOptions
            {
                UseNativeScanning = true
            };

            ScannerPage scannerPage = new ScannerPage(options);
            scannerPage.Appearing += ScannerPage_Appearing;
            scannerPage.Disappearing += ScannerPage_Disappearing;

            //Add tap to focus.
            TapGestureRecognizer tapToFocusGesture = new TapGestureRecognizer();
            tapToFocusGesture.Tapped += (o, e) => scannerPage.SetFocus();
            scannerPage.Overlay.GestureRecognizers.Add(tapToFocusGesture);

            scannerPage.OnScanResult += (result) =>
            {
                // Stop scanning
                scannerPage.IsScanning = false;
                Barcode = result.Text;

                DateTime scannedTime = DateTime.Now;
                TimeToScan = scannedTime - creationTime;

                // Pop the page and show the result
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await NavigationHelper.PopAsync();
                });
            };

            // Navigate to our scanner page
            await NavigationHelper.PushAsync(scannerPage);
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
        /// ScannerPage appearing event handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScannerPage_Appearing(object sender, System.EventArgs e)
        {
            IsScanning = true;
        }

        /// <summary>
        /// ScannerPage disappearing event handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScannerPage_Disappearing(object sender, System.EventArgs e)
        {
            IsScanning = false;
        }
    }
}