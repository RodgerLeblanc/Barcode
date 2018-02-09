using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace App2
{
    /// <summary>
    /// BaseViewModel overload with a Model.
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class BaseViewModel<TModel> : BaseViewModel
        where TModel : IModel
    {
        /// <summary>
        /// Private reference to the model.
        /// </summary>
        private TModel _model;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="model"></param>
        public BaseViewModel(TModel model)
        {
            _model = model;
        }

        /// <summary>
        /// Return the model.
        /// </summary>
        /// <returns></returns>
        protected virtual TModel ToModel()
        {
            return _model;
        }
    }

    /// <summary>
    /// BaseViewModel
    /// Provides PropertyChanged methods.
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Set a property and call OnPropertyChanged.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="storage"></param>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public bool SetProperty<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Object.Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// PropertyChanged event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notify every PropertyChanged event listeners.
        /// </summary>
        /// <param name="propertyName"></param>
        protected void OnPropertyChanged([CallerMemberName]string propertyName = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}