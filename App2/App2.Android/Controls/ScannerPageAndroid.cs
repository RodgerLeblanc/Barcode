using App2.Controls;
using App2.Helpers;
using System;
using Xamarin.Forms;

namespace App2.Droid.Controls
{
    /// <summary>
    /// ScannerPageAndroid
    /// </summary>
    public class ScannerPageAndroid : ScannerPage
    {
        /// <summary>
        /// OnScanStarted method to run platform specific code.
        /// </summary>
        protected override void OnScanStarted()
        {
            base.OnScanStarted();

            //Autofocus every 3 seconds.
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                if (!IsActive)
                {
                    //Stop the timer.
                    return false;
                }
                else
                {
                    try
                    {
                        AutoFocus();
                    }
                    catch (Exception e)
                    {
                        AlertHelper.DisplayAlert("Exception setting AutoFocus", e.Message + "\n\n" + e.StackTrace);
                    }
                }

                //Let the timer run.
                return true;
            });
        }
    }
}