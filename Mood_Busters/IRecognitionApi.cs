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
    //      Struct for emotion to be passed from the interface to the UI
    public struct Mood : IEquatable<Mood>
    {
        public MoodName Name;
        public float Confidence;

        public bool Equals(Mood other)
        {
            return this.Name == other.Name &&
                Math.Abs(this.Confidence - other.Confidence) <= 30; // account for 30 percent error
        }

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
        Mood GetMood(MemoryStream memStr);
    }
}
