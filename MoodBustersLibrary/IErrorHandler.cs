using System;

namespace MoodBustersLibrary
{
    public interface IErrorHandler
    {
        void ShowError(string errorText, string errorName = "Error");
        void HandleException(Exception e, string errorName = "Error");
        void HandleAndExit(string errorText, string errorName = "Error");
    }
}