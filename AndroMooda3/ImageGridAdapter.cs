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

namespace AndroMooda3
{
    class ImageGridAdapter : RecyclerView.Adapter
    {

        Context context;
        IEnumerable<ImageItem> images;

        public ImageGridAdapter(Context context, IEnumerable<ImageItem> images)
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
            var view = convertView;
            ImageGridAdapterViewHolder holder = null;

            if (view != null)
                holder = view.Tag as ImageGridAdapterViewHolder;

            if (holder == null)
            {
                holder = MakeHolder(holder, parent, view);
            }


            //fill in your items
            //holder.Title.Text = "new text here";

            return view;
        }

        private ImageGridAdapterViewHolder MakeHolder(ImageGridAdapterViewHolder holder, ViewGroup parent, View view)
        {
            var inflater = context.GetSystemService(Context.LayoutInflaterService).JavaCast<LayoutInflater>();
            //replace with your item and your holder items
            //comment back in
            view = inflater.Inflate(Resource.Layout.item_images, parent, false);
            holder = new ImageGridAdapterViewHolder(view);
            holder.Image = view.FindViewById<ImageView>(Resource.Id.image);
            holder.MoodChip = view.FindViewById(Resource.Id.mood_chip);
            view.Tag = holder;

            return holder;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            ImageItem item = images.ToList()[position];
            //(holder as ImageGridAdapterViewHolder).Image = item.img;
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            return MakeHolder(null, parent, null);
        }
    }

    class ImageGridAdapterViewHolder : RecyclerView.ViewHolder
    {
        public ImageView Image { get; set; }
        public View MoodChip { get; set; }
        public ImageGridAdapterViewHolder(View itemView) : base(itemView)
        {
        }
    }
}