namespace MoodBustersLibrary
{
    public interface IErrorHandler
    {
        void ShowError(string errorText, string errorName = "Error");
        void HandleAndExit(string errorText, string errorName = "Error");
    }
}