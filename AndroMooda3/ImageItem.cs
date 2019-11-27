using MoodBustersLibrary;
using System.Drawing;

namespace AndroMooda3
{
    internal class ImageItem
    {
        public Image img { get; set; }
        public MoodName mood { get; set; }

        public ImageItem(Image img, MoodName mood)
        {
            this.img = img;
            this.mood = mood;
        }
    }
}