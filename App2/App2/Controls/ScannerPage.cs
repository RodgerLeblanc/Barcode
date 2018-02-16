using App2.Helpers;
using App2.Models;
using System;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;
using ZXing;
using System.Collections.Generic;
using System.Linq;

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
        /// Private reference to the scanning options used with this instance.
        /// </summary>
        private ScanningOptionsModel _options = null;

        /// <summary>
        /// Private reference to the list of scanning results.
        /// </summary>
        private IList<Result> _results = new List<Result>();

        /// <summary>
        /// Occurs when the same barcode was detected [MultipleCheckCount] times.
        /// </summary>
        public new event ScanResultDelegate OnScanResult;

        /// <summary>
        /// Constructor
        /// </summary>
        public ScannerPage() : base()
        {
            _options = new ScanningOptionsModel();
            base.OnScanResult += ScannerPage_OnScanResult;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        public ScannerPage(ScanningOptionsModel options) : base(options)
        {
            _options = options;
            base.OnScanResult += ScannerPage_OnScanResult;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        /// <param name="customOverlay"></param>
        public ScannerPage(ScanningOptionsModel options, View customOverlay) : base(options, customOverlay)
        {
            _options = options;
            base.OnScanResult += ScannerPage_OnScanResult;
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
        /// OnAppearing override.
        /// </summary>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            SetCenterOfScreen();

            IsActive = true;

            if (_options.UseAutoFocusLoop && !_options.DisableAutofocus)
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
            if (_options.DisableAutofocus)
            {
                return;
            }

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

        /// <summary>
        /// ScannerPage OnScanResult callback.
        /// </summary>
        /// <param name="result">Result.</param>
        void ScannerPage_OnScanResult(Result result)
        {
            //If the barcode format received doesn't need multiple check, we can return a result right away.
            if (!_options.MultipleCheckForFormats.Any() || !_options.MultipleCheckForFormats.Contains(result.BarcodeFormat)) {
                this.OnScanResult?.Invoke(result);
                return;
            }

            //This is our first result, nothing to compare with yet.
            if (!_results.Any()) {
                _results.Add(result);
                return;
            }

            Result lastResult = _results.LastOrDefault();

            DateTime lastResultTime = DateTime.FromFileTimeUtc(lastResult.Timestamp);
            DateTime newResultTime = DateTime.FromFileTimeUtc(result.Timestamp);
            int interval = (newResultTime - lastResultTime).Milliseconds;

            //If we have the same result [MultipleCheckCount] times, we can return the result.
            if (interval > _options.MinimumMultipleCheckInterval && 
                _results.Where(GetMatchPredicate(result)).Count() >= _options.MultipleCheckCount - 1)
            {
                this.OnScanResult?.Invoke(result);

                _results.Clear();
            }
            else
            {
                _results.Add(result);
            }
        }

        /// <summary>
        /// Returns a predicate that checks if an item match the specified result.
        /// </summary>
        /// <returns></returns>
        /// <param name="result"></param>
        private Func<Result, bool> GetMatchPredicate(Result result)
        {
            return r => r.BarcodeFormat == result.BarcodeFormat && r.Text == result.Text;
        }
    }
}
