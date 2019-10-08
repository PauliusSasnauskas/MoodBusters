using System;
using System.Collections.Generic;
using System.IO;

namespace Mood_Busters
{
    /// <summary>
    /// Interface to unify all APIs to use same function signature
    /// </summary>
    interface IRecognitionApi
    {
        List<Mood> GetMoods(MemoryStream memStr);
    }
}
