using MoodBustersLibrary;
using Android.Graphics;

namespace AndroMooda3
{
    internal class ImageItem
    {
        public Bitmap img { get; set; }
        public MoodName mood { get; set; }

        public ImageItem(Bitmap img, MoodName mood)
        {
            this.img = img;
            this.mood = mood;
        }
    }
}