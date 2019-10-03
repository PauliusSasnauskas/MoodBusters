using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mood_Busters
{
    class ErrorHandlerAndroid : IErrorHandler
    {

        public void ShowError(string errorText, string errorName = "Error")
        {
            //For future android error handler
            //Toast.MakeText(this, errorText, ToastLength.Long).Show(); 
        }

        public void HandleAndExit(string errorText, string errorName = "Error")
        {
            ShowError(errorText, errorName);
            //Application.exit();
        }
    }
}