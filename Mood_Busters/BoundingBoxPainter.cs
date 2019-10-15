using MoodBustersLibrary;
using System.Collections.Generic;
using System.Drawing;

namespace Mood_Busters
{
    public abstract class BoundingBoxPainter
    {
        public Image Image { get; protected set; }
        public void PaintAll(List<Mood> moods)
        {
            moods.ForEach(Paint);
        }
        public abstract void Paint(Mood mood);
    }
}
