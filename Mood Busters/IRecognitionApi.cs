using System;
using System.IO;

namespace Mood_Busters
{
    //
    // Summary:
    //     Enum to unify all defining moods of a response
    public enum MoodName { Error, Unknown, Happy, Sad, Angry, Confused, Disgusted, Surprised, Calm, Fear }

    //
    // Summary:
    //     Struct for emotion to be passed from the interface to the UI
    public struct Mood
    {
        public MoodName Name;
        public float Confidence;

        public override string ToString()
        {
            return $"{Name.ToString()} {Confidence.ToString("0")}%";
        }
    }


    //
    // Summary:
    //     Interface to unify all APIs to use same function signature
    interface IRecognitionApi
    {
        //
        Mood GetMood(string imageLocation);
        Mood GetMood(MemoryStream memStr);

    }
}
