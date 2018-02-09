using App2.Helpers;
using System;
using Xamarin.Forms;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace App2.Controls
{
    /// <summary>
    /// ScannerPage
    /// </summary>
    public class ScannerPage : ZXingScannerPage
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public ScannerPage() : base()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        public ScannerPage(MobileBarcodeScanningOptions options) : base(options)
        {
        }

        /// <summary>
        /// IsActive
        /// </summary>
        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        /// <summary>
        /// IsActive bindable property.
        /// </summary>
        public static readonly BindableProperty IsActiveProperty =
            BindableProperty.CreateAttached("IsActive", typeof(bool), typeof(ScannerPage), false, BindingMode.OneWayToSource);

        /// <summary>
        /// UseAutoFocus
        /// </summary>
        public bool UseAutoFocus { get; set; } = Device.RuntimePlatform == Device.Android;

        /// <summary>
        /// OnAppearing override.
        /// </summary>
        protected override void OnAppearing()
        {
            IsActive = true;

            base.OnAppearing();

            if (UseAutoFocus)
            {
                OnScanStarted();
            }
        }

        /// <summary>
        /// OnDisappearing override.
        /// </summary>
        protected override void OnDisappearing()
        {
            IsActive = false;

            base.OnDisappearing();
        }

        /// <summary>
        /// OnScanStarted method to run platform specific code.
        /// </summary>
        protected virtual void OnScanStarted()
        {
            //Autofocus every 3 seconds.
            Device.StartTimer(TimeSpan.FromSeconds(3), () =>
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
