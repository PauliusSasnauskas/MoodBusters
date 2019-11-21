using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Castle.DynamicProxy;
using MoodBustersLibrary;
using Plugin.CurrentActivity;

namespace AndroMooda3
{
    class ErrorAspect : Attribute, IInterceptor 
    {
        private IErrorHandler errorHandler = new ErrorHandlerAndroid(CrossCurrentActivity.Current.Activity);

        public void Intercept(IInvocation invocation)
        {
            try
            {
                invocation.Proceed();
            }
            catch (Exception ex)
            {
                errorHandler.ShowError(ex.Message);
            }
        }
    }
}