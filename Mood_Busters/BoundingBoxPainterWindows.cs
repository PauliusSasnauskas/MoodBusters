using MoodBustersLibrary;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;

namespace Mood_Busters
{
    class BoundingBoxPainterWindows : BoundingBoxPainter
    {
        private Graphics Graphics;
        private SolidBrush Brush;
        private SolidBrush BrushFont;
        public BoundingBoxPainterWindows(MemoryStream stream)
        {
            Image = new Bitmap(stream);
            Graphics = Graphics.FromImage(Image);
            Brush = new SolidBrush(Color.FromArgb(180, 100, 100, 220));
            BrushFont = new SolidBrush(Color.FromArgb(255, 180, 180, 255));
        }

        public override void Paint(Mood mood)
        {
            if (!RegexStringCheck.checkString(mood.ToString()))
            {
                return;
            }
            GraphicsPath DrawRoundedRectangle(float x, float y,
                float width, float height, float r)
            {
                GraphicsPath Path = new GraphicsPath();

                float d = r * 2;
                float dX = x + width;
                float dY = y + height;
                Path.AddLine(x + r, y, dX - d, y);
                Path.AddArc(dX - d, y, d, d, 270, 90);
                Path.AddLine(dX, y + r, dX, dY - d);
                Path.AddArc(dX - d, dY - d, d, d, 0, 90);
                Path.AddLine(dX - d, dY, x+r, dY);
                Path.AddArc(x, dY - d, d, d, 90, 90);
                Path.AddLine(x, dY - d, x, y+r);
                Path.AddArc(x, y, d, d, 180, 90);
                return Path; 
            }

            GraphicsPath path = DrawRoundedRectangle( 
                mood.Left * Image.Width,
                mood.Top * Image.Height,
                mood.Width * Image.Width,
                mood.Height * Image.Height,
                50);

            Graphics.DrawPath(new Pen(Brush, 2), path);

            StringFormat sf = new StringFormat();
            sf.Alignment = sf.LineAlignment = StringAlignment.Center;

            Graphics.DrawString(mood.ToString(), new Font("Bahnschrift", mood.Width*Image.Width/8), BrushFont, 
                (mood.Left + mood.Width / 2) * Image.Width, (mood.Top - mood.Height / 10) * Image.Height, sf);
            Graphics.Save();
        }
    }
}
