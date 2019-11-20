using MoodBustersLibrary;
using Android.App;
using Android.Content;

namespace AndroMooda3
{
    public class ErrorHandlerAndroid : IErrorHandler
    {
        private Context context;

        public ErrorHandlerAndroid(Activity context)
        {
            this.context = context;
        }

        public void ShowError(string errorText, string errorName = "Error")
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(context);
            alert.SetTitle(errorName);
            alert.SetMessage(errorText);

            Dialog dialog = alert.Create();
            dialog.Show();
        }

        public void HandleAndExit(string errorText, string errorName = "Error")
        {
            ShowError(errorText, errorName);
            //Application.exit();
        }
    }
}