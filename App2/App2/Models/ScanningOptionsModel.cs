using Xamarin.Forms;
using ZXing.Mobile;

namespace App2.Models
{
    /// <summary>
    /// ScanningOptionsModel
    /// </summary>
    public class ScanningOptionsModel : MobileBarcodeScanningOptions, IModel
    {
        /// <summary>
        /// UseAutoFocusLoop
        /// </summary>
        public bool UseAutoFocusLoop { get; set; } = Device.RuntimePlatform == Device.Android;
    }
}
