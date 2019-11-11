using MoodBustersLibrary;
using System;
using System.Windows.Forms;

namespace Mood_Busters
{
    public class ErrorHandlerWindows : IErrorHandler
    {
        void GenericMethod<T>(ref T eT, ref T eR)
        {
            MessageBox.Show(eT.ToString(), eR.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ShowError(string errorText, string errorName = "Error")
        {
            //MessageBox.Show(errorText, errorName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            GenericMethod(ref errorText, ref errorName); //Now uses Generic Method to print out the same thing
        }

        public void HandleAndExit(string errorText, string errorName = "Error")
        {
            ShowError(errorText, errorName);
            Environment.Exit(0);
        }
    }
}