using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mood_Busters
{
    class ErrorHandlerAndroid : IErrorHandler
    {
        public void GetErrorType(string errorText)
        {
            //For future android error handler
            //Toast.MakeText(this, errorText, ToastLength.Long).Show(); 
        }

        public void GetErrorType(string errorText, string errorName)
        {
            //For future android error handler
            //Toast.MakeText(this, errorText, ToastLength.Long).Show(); 
        }
    }
}
