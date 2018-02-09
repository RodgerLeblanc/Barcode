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
        }

        /// <summary>
        /// ZXing
        /// </summary>
        public ZXingViewModel ZXing { get; set; }
    }
}