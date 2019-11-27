using MoodBustersLibrary;
using Android.App;
using Android.Content;
using Castle.DynamicProxy;
using Plugin.CurrentActivity;
using Autofac;

namespace AndroMooda3
{
    public class ErrorHandlerAndroid : IErrorHandler, IInterceptor
    {
        private Context context = CrossCurrentActivity.Current.Activity;

        public void Intercept(IInvocation invocation)
        {
            var b = new ContainerBuilder();
            ​
            //b.Register(i => new Logger(Console.Out));
            //b.RegisterType().As().EnableInterfaceInterceptors();
            ​
            var container = b.Build();
        }

        public void ShowError(string errorText, string errorName = "Error")
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(context);
            alert.SetTitle(errorName);
            alert.SetMessage(errorText);
            alert.Show();

            //Dialog dialog = alert.Create();
            //dialog.Show();
        }

        public void HandleAndExit(string errorText, string errorName = "Error")
        {
            ShowError(errorText, errorName);
            //Application.exit();
        }
    }
}