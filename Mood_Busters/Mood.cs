﻿using System;

namespace Mood_Busters
{
    //
    // Summary:
    //     Enum to unify all defining moods of a response
    public enum MoodName { Error, Unknown, Happy, Sad, Angry, Confused, Disgusted, Surprised, Calm, Fear }

    //
    // Summary:
    //      Struct for emotion to be passed from the interface to the UI
    public sealed class Mood : IEquatable<Mood>
    {
        public MoodName Name;
        public float Confidence;
        public float Top;
        public float Left;
        public float Width;
        public float Height;

        public bool Equals(Mood other)
        {
            return Name == other.Name &&
                Math.Abs(Confidence - other.Confidence) <= 30; // account for 30 percent error
        }

        public override string ToString()
        {
            return $"{Name.ToString()} {Confidence.ToString("0")}%";
        }
    }

}
