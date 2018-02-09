namespace App2.Helpers
{
    /// <summary>
    /// AlertHelper
    /// </summary>
    public static class AlertHelper
    {
        /// <summary>
        /// Display an alert.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="cancel"></param>
        public static void DisplayAlert(string title, string message, string cancel)
        {
            NavigationHelper.GetNavigationPage().DisplayAlert(title, message, cancel);
        }
    }
}
