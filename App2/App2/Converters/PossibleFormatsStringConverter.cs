using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;

namespace App2.Converters
{
    /// <summary>
    /// PossibleFormatsStringConverter
    /// </summary>
    public class PossibleFormatsStringConverter : IMarkupExtension, IValueConverter
    {
        /// <summary>
        /// Private reference to the char separator.
        /// </summary>
        private const char _charSeparator = ',';

        /// <summary>
        /// Convert a list of BarcodeFormat to a string.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is List<BarcodeFormat>))
            {
                return null;
            }

            List<BarcodeFormat> formats = (List<BarcodeFormat>)value;
            return String.Join(_charSeparator.ToString(), formats.Select(f => f.ToString()));
        }

        /// <summary>
        /// Convert a string to a list of BarcodeFormat.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string))
            {
                return new List<BarcodeFormat>();
            }

            return value.ToString()
                .Split(_charSeparator)
                .Where(s => !String.IsNullOrEmpty(s))
                .Select(s =>
                {
                    return Enum.TryParse(s, true, out BarcodeFormat format) ?
                        format :
                        default(BarcodeFormat?);
                })
                .Where(f => f.HasValue)
                .ToList();
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
