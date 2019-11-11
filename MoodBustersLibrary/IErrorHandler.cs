namespace MoodBustersLibrary
{
    public interface IErrorHandler
    {
        void GenericMethod<T>(ref T eT, ref T eR);
        void ShowError(string errorText, string errorName = "Error");
        void HandleAndExit(string errorText, string errorName = "Error");
    }
}