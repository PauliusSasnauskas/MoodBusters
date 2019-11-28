using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Views;
using MoodBustersLibrary;
using Plugin.CurrentActivity;

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

            layoutManager = new LinearLayoutManager(this);
            recyclerView.SetLayoutManager(layoutManager);

            IEnumerable<ImageItem> items = new List<ImageItem>
            {
                new ImageItem(null, MoodName.Happy),
            };

            imageAdapter = new ImageGridAdapter(this, items);
            recyclerView.SetAdapter(imageAdapter);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.btn_add_image);
            fab.Click += (sender, eventArgs) => {
                errorHandler.HandleAndExit("Something went wrong...");
            };
        }
    }

}