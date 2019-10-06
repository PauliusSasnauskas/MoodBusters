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
    //      Struct for emotion to be passed from the image recognition api to the user
    public struct Mood : IEquatable<Mood>
    {
        public MoodName Name { get; set; }
        private float _Confidence;
        public float Confidence
        {
            get => _Confidence;
            set
            {
                if (value < 0f) { _Confidence = 0; }
                else if (value > 100f) { _Confidence = 100; }
                else { _Confidence = (float)value; }    // Forces the value to be in [0, 100]
            }
        }

        public bool Equals(Mood other)
        {
            return this.Name == other.Name &&
                Math.Abs(this.Confidence - other.Confidence) <= 30; // account for 30 percent error
        }

        public override string ToString()
        {
            return $"{Name.ToString()} {Confidence.ToString("0")}%";
        }

        public MoodName this[int index]
        {
            get
            {
                if (index == 0)
                {
                    return Name;
                }
                else
                {
                    return MoodName.Error;
                }
            }
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
