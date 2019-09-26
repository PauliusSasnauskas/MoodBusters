using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mood_Busters
{
    class ErrorHandlerWindows : IErrorHandler
    {
        public void GetErrorType(string errorText)
        {
            MessageBox.Show(errorText, "Error" , MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public void GetErrorType(string errorText, string errorName)
        {
            MessageBox.Show(errorText, errorName, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
