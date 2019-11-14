using MoodBustersLibrary;
using System;
using System.Windows.Forms;

namespace Mood_Busters
{
    public class ErrorHandlerWindows : IErrorHandler
    {
        public void GenericMethod<T>(T eT, T eR) //Generic Method
        {
            T errorText = eT;
            T errorName = eR;
            MessageBox.Show(errorText.ToString(), errorName.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public delegate string GenericDelegate<TT>(TT function); //Generic Delegate
        public static string SimpleDelegate(string text)
        {
            return text;
        }
        public void ShowError(string errorText, string errorName = "Error")
        {
            GenericMethod(errorText, errorName); //Now uses Generic Method to print out the same thing
            //MessageBox.Show(errorText, errorName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void HandleAndExit(string errorText, string errorName = "Error")
        {
            GenericDelegate<string> errorDelegate = new GenericDelegate<string>(SimpleDelegate);           

            ShowError(errorDelegate(errorText), errorDelegate(errorName));  //Now uses Generic Delegates to print out the error
            //ShowError(errorText,errorName)
            Environment.Exit(0);
        }
    }
}