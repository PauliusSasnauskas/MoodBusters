using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Mood_Busters
{
    class BoundingBoxPainterWindows : BoundingBoxPainter
    {
        private Graphics Graphics;
        private SolidBrush Brush;
        private SolidBrush BrushFont;
        private Font Font;
        public BoundingBoxPainterWindows(MemoryStream stream)
        {
            Image = new Bitmap(stream);
            Graphics = Graphics.FromImage(Image);
            Brush = new SolidBrush(Color.FromArgb(64, 255, 255, 255));
            BrushFont = new SolidBrush(Color.FromArgb(192, 255, 255, 255));
            Font = new Font("Arial", 20);
        }

        public override void Paint(Mood mood)
        {
            Graphics.FillRectangle(Brush, new RectangleF(
                    mood.Left * Image.Width,
                    mood.Top * Image.Height,
                    mood.Width * Image.Width,
                    mood.Height * Image.Height));
            Graphics.DrawString(mood.ToString(), Font, BrushFont, mood.Left * Image.Width - 4, mood.Top * Image.Height - 30);
            Graphics.Save();
        }
    }
}
