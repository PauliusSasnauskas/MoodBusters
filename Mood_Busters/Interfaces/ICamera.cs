using System;

namespace Mood_Busters
{
    interface ICameraBox
    {
        void StreamFrames(object sender, EventArgs e);
        void ResumeCamera();
        void StopCamera();
    }
}