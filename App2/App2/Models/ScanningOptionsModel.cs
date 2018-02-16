using System.Collections.Generic;
using Xamarin.Forms;
using ZXing;
using ZXing.Mobile;

namespace App2.Models
{
    /// <summary>
    /// ScanningOptionsModel
    /// </summary>
    public class ScanningOptionsModel : MobileBarcodeScanningOptions, IModel
    {
        /// <summary>
        /// MinimumMultipleCheckInterval
        /// </summary>
        public int MinimumMultipleCheckInterval { get; set; } = 100;

        /// <summary>
        /// MultipleCheckCount
        /// </summary>
        public int MultipleCheckCount { get; set; } = 2;

        /// <summary>
        /// MultipleCheckForFormats
        /// </summary>
        public List<BarcodeFormat> MultipleCheckForFormats { get; set; } = new List<BarcodeFormat>() { BarcodeFormat.UPC_E };

        /// <summary>
        /// UseAutoFocusLoop
        /// </summary>
        public bool UseAutoFocusLoop { get; set; } = Device.RuntimePlatform == Device.Android;
    }
}
