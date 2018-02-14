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
        /// Private reference to the calculated center of the screen.
        /// </summary>
        private Point _centerOfScreen;

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
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        /// <param name="customOverlay"></param>
        public ScannerPage(MobileBarcodeScanningOptions options, View customOverlay) : base(options, customOverlay)
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
            base.OnAppearing();

            SetCenterOfScreen();

            IsActive = true;

            if (UseAutoFocus)
            {
                OnScanStarted();
            }
        }

        /// <summary>
        /// Save the center of screen point.
        /// </summary>
        private void SetCenterOfScreen()
        {
            _centerOfScreen = new Point(Width / 2, Height / 2);
        }

        /// <summary>
        /// Set focus manually.
        /// </summary>
        public void SetFocus()
        {
            try
            {
                if (_centerOfScreen.IsEmpty || _centerOfScreen.X <= 0 || _centerOfScreen.Y <= 0)
                {
                    AutoFocus();
                }
                else
                {
                    AutoFocus(Convert.ToInt32(_centerOfScreen.X), Convert.ToInt32(_centerOfScreen.Y));
                }
            }
            catch (Exception e)
            {
                AlertHelper.DisplayAlert("Exception setting AutoFocus", e.Message + "\n\n" + e.StackTrace);
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
        /// OnScanStarted method, can be override to run platform specific code.
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
                    SetFocus();
                }

                //Let the timer run.
                return true;
            });
        }
    }
}
