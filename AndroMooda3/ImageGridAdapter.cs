using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using MoodBustersLibrary;

namespace AndroMooda3
{
    class ImageGridAdapter : RecyclerView.Adapter
    {

        Context context;
        List<ImageItem> images;

        public ImageGridAdapter(Context context, List<ImageItem> images)
        {
            this.context = context;
            this.images = images;
        }

        public override int ItemCount => images.Count();

        public override long GetItemId(int position)
        {
            return position;
        }

        public View GetView(int position, View convertView, ViewGroup parent)
        {
            ImageGridAdapterViewHolder holder = null;

            if (convertView != null)
                holder = convertView.Tag as ImageGridAdapterViewHolder;

            if (holder == null)
            {
                holder = MakeHolder(holder, parent, convertView);
            }

            holder.Item = images[position];

            return convertView;
        }

        private ImageGridAdapterViewHolder MakeHolder(ImageGridAdapterViewHolder holder, ViewGroup parent, View view)
        {
            var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
            //replace with your item and your holder items
            //comment back in
            view = inflater.Inflate(Resource.Layout.item_images, parent, false);
            view.LayoutParameters.Height = parent.MeasuredWidth / 4;
            holder = new ImageGridAdapterViewHolder(view);
            view.Tag = holder;

            return holder;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            ImageItem item = images.ToList()[position];
            (holder as ImageGridAdapterViewHolder).Item = item;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            return MakeHolder(null, parent, null);
        }
    }

    class ImageGridAdapterViewHolder : RecyclerView.ViewHolder
    {
        private View itemView;
        public ImageView Image { get; set; }
        public View MoodChip { get; set; }

        private ImageItem _Item;
        public ImageItem Item {
            get
            {
                return _Item;
            }
            internal set
            {
                if (Image == null || MoodChip == null) Initialize();
                _Item = value;
                Image.SetImageBitmap(_Item.img);
                MoodChip.SetBackgroundResource(GetColorFromMood(_Item.mood));
            }
        }

        private static int GetColorFromMood(MoodName mood)
        {
            switch (mood)
            {
                case MoodName.Angry: return Resource.Color.colorAngry;
                case MoodName.Calm: return Resource.Color.colorCalm;
                case MoodName.Confused: return Resource.Color.colorConfused;
                case MoodName.Disgusted: return Resource.Color.colorDisgusted;
                case MoodName.Fear: return Resource.Color.colorFear;
                case MoodName.Happy: return Resource.Color.colorHappy;
                case MoodName.Sad: return Resource.Color.colorSad;
                case MoodName.Surprised: return Resource.Color.colorSurprised;
                default: return Resource.Color.colorNeutral;
            }
        }

        public ImageGridAdapterViewHolder(View itemView) : base(itemView)
        {
            this.itemView = itemView;
            Initialize();
        }

        private void Initialize()
        {
            Image = itemView.FindViewById<ImageView>(Resource.Id.mood_image);
            MoodChip = itemView.FindViewById(Resource.Id.mood_chip);
        }
    }
}