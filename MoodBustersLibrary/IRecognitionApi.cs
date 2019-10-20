using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace MoodBustersLibrary
{
    /// <summary>
    /// Interface to unify all APIs to use same function signature
    /// </summary>
    public interface IRecognitionApi
    {
        Task<List<Mood>> GetMoodsAsync(MemoryStream memStr);
    }
}
