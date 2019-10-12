using System;

namespace MoodBustersLibrary
{
    public interface ICameraBox
    {
        void StreamFrames(object sender, EventArgs e);
        void ResumeCamera();
        void StopCamera();
    }
}