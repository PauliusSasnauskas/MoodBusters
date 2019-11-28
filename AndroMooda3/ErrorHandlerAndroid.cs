using MoodBustersLibrary;
using Android.App;
using Plugin.CurrentActivity;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AndroMooda3
{
    public class ErrorHandlerAndroid : IErrorHandler
    {
        private Activity activity = CrossCurrentActivity.Current.Activity;

        public void ShowError(string errorText, string errorName = "Error")
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(activity);
            alert.SetTitle(errorName);
            alert.SetMessage(errorText);
            alert.Show();
        }

        public async void HandleAndExit(string errorText, string errorName = "Error")
        {
            ShowError(errorText, errorName);
            await Task.Delay(TimeSpan.FromSeconds(2));
            activity.FinishAffinity();
        }

        public void HandleException(Exception e, string errorName = "Error")
        {
            ShowError(e.Message, errorName);

            if(e.InnerException != null)
            {
                HandleException(e.InnerException, errorName);
            }
        }

    }
}