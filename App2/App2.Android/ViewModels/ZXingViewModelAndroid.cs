using System;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace App2.Droid.ViewModels
{
    /// <summary>
    /// ZXingViewModelAndroid
    /// </summary>
    public class ZXingViewModelAndroid : ZXingViewModel
    {
        /// <summary>
        /// Run Android specific code on scan started.
        /// </summary>
        /// <param name="scanPage"></param>
        protected override void OnScanStarted(ZXingScannerPage scanPage)
        {
            base.OnScanStarted(scanPage);

            //Autofocus every 3 seconds.
            Device.StartTimer(TimeSpan.FromSeconds(3), () =>
            {
                if (scanPage == null || !scanPage.IsScanning)
                {
                    //Stop the timer.
                    return false;
                }
                else
                {
                    scanPage.AutoFocus();
                }

                //Let the timer run.
                return true;
            });
        }
    }
}