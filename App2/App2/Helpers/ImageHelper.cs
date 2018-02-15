using System;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2.Helpers
{
    /// <summary>
    /// ImageHelper
    /// </summary>
    [ContentProperty("Source")]
    public class ImageHelper : IMarkupExtension
    {
        public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Source == null)
            {
                return null;
            }

            var imageSource = ImageSource.FromResource("App2.Images." + Source, typeof(App).GetTypeInfo().Assembly);

            return imageSource;
        }
    }
}
