namespace MoodBustersLibrary
{
    public interface IErrorHandler
    {
        void GenericMethod<T>(T eT, T eR);
        void ShowError(string errorText, string errorName = "Error");
        void HandleAndExit(string errorText, string errorName = "Error");
    }
}