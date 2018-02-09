using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App2.Helpers
{
    /// <summary>
    /// NavigationHelper
    /// </summary>
    public static class NavigationHelper
    {
        /// <summary>
        /// Push a page asynchronously.
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public static async Task PushAsync(Page page)
        {
            try
            {
                await GetNavigationPage().PushAsync(page, false);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error pushing a page: " + ex.Message);
            }
        }

        /// <summary>
        /// Pop a page asynchronously.
        /// </summary>
        /// <returns></returns>
        public static async Task PopAsync()
        {
            await GetNavigationPage().PopAsync(false);
        }

        /// <summary>
        /// Get the base NavigationPage.
        /// </summary>
        /// <returns></returns>
        public static NavigationPage GetNavigationPage()
        {
            var mainPage = Application.Current.MainPage;

            if (!(mainPage is NavigationPage))
                throw new Exception("The application isn't a NavigationPage");

            return mainPage as NavigationPage;
        }
    }
}