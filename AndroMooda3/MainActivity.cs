using System;
using System.Linq;
using System.Collections.Generic;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using MoodBustersLibrary;
using Plugin.CurrentActivity;
using System.Text;
using Android.Content;
using Android.Widget;

namespace AndroMooda3
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : Activity
    {
        RecyclerView recyclerView;
        RecyclerView.LayoutManager layoutManager;
        RecyclerView.Adapter imageAdapter;
        IErrorHandler errorHandler;
        ImageList imageList;

        public static readonly int PickImageId = 1000;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);

            errorHandler = new ErrorHandlerAndroid();

            SetContentView(Resource.Layout.activity_main);

            recyclerView = FindViewById<RecyclerView>(Resource.Id.list_images);

            layoutManager = new GridLayoutManager(this, 4);
            recyclerView.SetLayoutManager(layoutManager);

            List<ImageItem> images = new List<ImageItem>{
                    new ImageItem(BitmapFactory.DecodeResource(Resources, Resource.Drawable.face1), MoodName.Happy),
                    new ImageItem(BitmapFactory.DecodeResource(Resources, Resource.Drawable.face2), MoodName.Calm),
                    new ImageItem(BitmapFactory.DecodeResource(Resources, Resource.Drawable.face3), MoodName.Angry),
                    new ImageItem(BitmapFactory.DecodeResource(Resources, Resource.Drawable.face4), MoodName.Sad),
                    new ImageItem(BitmapFactory.DecodeResource(Resources, Resource.Drawable.face5), MoodName.Disgusted),
            };

            imageList = new ImageList();
            imageList.Images.AddRange(images);

            //imageList.Images = imageList.Images.Skip(4).Take(25).ToList();


            string feelings = imageList.Images.Aggregate("People are feeling ", (string acc, ImageItem item) => acc + item.mood.ToString() + ", ");

            Snackbar.Make(recyclerView.RootView, feelings, Snackbar.LengthShort).Show();

            imageAdapter = new ImageGridAdapter(this, imageList.Images);
            recyclerView.SetAdapter(imageAdapter);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.btn_add_image);
            fab.Click += (sender, eventArgs) => {
                Intent = new Intent();
                Intent.SetType("image/*");
                Intent.SetAction(Intent.ActionGetContent);
                StartActivityForResult(Intent.CreateChooser(Intent, "Select Picture"), PickImageId);
            };  
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if ((requestCode == PickImageId) && (resultCode == Result.Ok) && (data != null))
            {
                Android.Net.Uri uri = data.Data;
                imageList.AddImage(uri);
                imageAdapter.NotifyDataSetChanged();
            }
            else Toast.MakeText(this, "No image was added.", ToastLength.Short).Show();
        }
    }

}