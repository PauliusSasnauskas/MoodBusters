using System;
using System.Collections.Generic;
using System.IO;

namespace Mood_Busters
{
    //
    // Summary:
    //     Interface to unify all APIs to use same function signature
    interface IRecognitionApi
    {
        List<Mood> GetMoods(MemoryStream memStr);
    }
}
