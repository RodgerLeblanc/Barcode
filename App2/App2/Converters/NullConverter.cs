using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2.Converters
{
    public class NullConverter : IMarkupExtension, IValueConverter
    {
        /// <summary>
        /// TrueValue
        /// </summary>
        public object TrueValue { get; set; } = true;

        /// <summary>
        /// FalseValue
        /// </summary>
        public object FalseValue { get; set; } = false;

        /// <summary>
        /// Return a different object depending if the binded object is null or not.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? TrueValue : FalseValue;
        }

        /// <summary>
        /// Not used.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Provide the value.
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
