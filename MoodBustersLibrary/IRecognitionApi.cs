using System.Collections.Generic;
using System.IO;

namespace MoodBustersLibrary
{
    /// <summary>
    /// Interface to unify all APIs to use same function signature
    /// </summary>
    public interface IRecognitionApi
    {
        List<Mood> GetMoods(MemoryStream memStr);
    }
}
