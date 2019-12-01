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

namespace AndroMooda3
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : Activity
    {
        RecyclerView recyclerView;
        RecyclerView.LayoutManager layoutManager;
        RecyclerView.Adapter imageAdapter;
        IErrorHandler errorHandler;

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

            List<ImageItem> items = new List<ImageItem>();
            for (int i = 0; i < 6; i++)
            {
                items.AddRange(new List<ImageItem>{
                    new ImageItem(BitmapFactory.DecodeResource(Resources, Resource.Drawable.face1), MoodName.Happy),
                    new ImageItem(BitmapFactory.DecodeResource(Resources, Resource.Drawable.face2), MoodName.Calm),
                    new ImageItem(BitmapFactory.DecodeResource(Resources, Resource.Drawable.face3), MoodName.Angry),
                    new ImageItem(BitmapFactory.DecodeResource(Resources, Resource.Drawable.face4), MoodName.Sad),
                    new ImageItem(BitmapFactory.DecodeResource(Resources, Resource.Drawable.face5), MoodName.Disgusted)
                });
            }

            imageAdapter = new ImageGridAdapter(this, items);
            recyclerView.SetAdapter(imageAdapter);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.btn_add_image);
            fab.Click += (sender, eventArgs) => {
                errorHandler.HandleAndExit("Something went wrong...");
            };
        }
    }

}