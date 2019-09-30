using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mood_Busters
{
    interface IErrorHandler
    {
        void ShowError(string errorText, string errorName = "Error");
    }
}