using App2.Models;
using System.Collections.Generic;
using ZXing;

namespace App2.ViewModels
{
    /// <summary>
    /// ScanningOptionsViewModel
    /// </summary>
    public class ScanningOptionsViewModel : BaseViewModel<ScanningOptionsModel>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="model"></param>
        public ScanningOptionsViewModel(ScanningOptionsModel model) : base(model)
        {
        }

        /// <summary>
        /// AssumeGS1
        /// </summary>
        public bool AssumeGS1
        {
            get { return Model.AssumeGS1.GetValueOrDefault(); }
            set { Model.AssumeGS1 = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// AutoRotate
        /// </summary>
        public bool AutoRotate
        {
            get { return Model.AutoRotate.GetValueOrDefault(); }
            set { Model.AutoRotate = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// DelayBetweenAnalyzingFrames
        /// </summary>
        public int DelayBetweenAnalyzingFrames
        {
            get { return Model.DelayBetweenAnalyzingFrames; }
            set { Model.DelayBetweenAnalyzingFrames = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// DelayBetweenContinuousScans
        /// </summary>
        public int DelayBetweenContinuousScans
        {
            get { return Model.DelayBetweenContinuousScans; }
            set { Model.DelayBetweenContinuousScans = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// DisableAutofocus
        /// </summary>
        public bool DisableAutofocus
        {
            get { return Model.DisableAutofocus; }
            set { Model.DisableAutofocus = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// InitialDelayBeforeAnalyzingFrames
        /// </summary>
        public int InitialDelayBeforeAnalyzingFrames
        {
            get { return Model.InitialDelayBeforeAnalyzingFrames; }
            set { Model.InitialDelayBeforeAnalyzingFrames = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// PossibleFormats
        /// </summary>
        public List<BarcodeFormat> PossibleFormats
        {
            get { return Model.PossibleFormats; }
            set { Model.PossibleFormats = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// PureBarcode
        /// </summary>
        public bool PureBarcode
        {
            get { return Model.PureBarcode.GetValueOrDefault(); }
            set { Model.PureBarcode = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// TryHarder
        /// </summary>
        public bool TryHarder
        {
            get { return Model.TryHarder.GetValueOrDefault(); }
            set { Model.TryHarder = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// TryInverted
        /// </summary>
        public bool TryInverted
        {
            get { return Model.TryInverted.GetValueOrDefault(); }
            set { Model.TryInverted = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// UseAutoFocusLoop
        /// </summary>
        public bool UseAutoFocusLoop
        {
            get { return Model.UseAutoFocusLoop; }
            set { Model.UseAutoFocusLoop = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// UseCode39ExtendedMode
        /// </summary>
        public bool UseCode39ExtendedMode
        {
            get { return Model.UseCode39ExtendedMode.GetValueOrDefault(); }
            set { Model.UseCode39ExtendedMode = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// UseFrontCameraIfAvailable
        /// </summary>
        public bool UseFrontCameraIfAvailable
        {
            get { return Model.UseFrontCameraIfAvailable.GetValueOrDefault(); }
            set { Model.UseFrontCameraIfAvailable = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// UseNativeScanning
        /// </summary>
        public bool UseNativeScanning
        {
            get { return Model.UseNativeScanning; }
            set { Model.UseNativeScanning = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Returns the model.
        /// </summary>
        /// <returns></returns>
        internal override ScanningOptionsModel ToModel()
        {
            return base.ToModel();
        }
    }
}
