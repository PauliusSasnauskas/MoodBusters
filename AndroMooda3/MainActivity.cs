using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Button;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using MoodBustersLibrary;

namespace AndroMooda3
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : Activity
    {
        RecyclerView recyclerView;
        RecyclerView.LayoutManager layoutManager;
        RecyclerView.Adapter imageAdapter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);

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
        }
    }
}