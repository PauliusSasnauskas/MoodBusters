using System.Collections.Generic;
using System.Linq;
using Android.Content.Res;
using Android.Graphics;
using Android.Net;
using Android.Provider;
using MoodBustersLibrary;
using Plugin.CurrentActivity;

namespace AndroMooda3
{
    class ImageList
    {
        public List<ImageItem> Images { get; set; } = new List<ImageItem>();

        public void AddImage(Uri url) {
            Bitmap bitmap = MediaStore.Images.Media.GetBitmap(CrossCurrentActivity.Current.Activity.ContentResolver, url);
            Images.Add(new ImageItem(bitmap, MoodName.Angry));
        }
    }
}