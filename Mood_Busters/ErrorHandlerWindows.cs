using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mood_Busters
{
    public class ErrorHandlerWindows : IErrorHandler
    {
        public void ShowError(string errorText, string errorName = "Error")
        {
            MessageBox.Show(errorText, errorName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void HandleAndExit(string errorText, string errorName = "Error")
        {
            ShowError(errorText, errorName);
            Environment.Exit(0);
        }
    }
}