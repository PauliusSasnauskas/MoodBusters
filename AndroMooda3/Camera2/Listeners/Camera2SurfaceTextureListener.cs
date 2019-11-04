using Android.Graphics;
using Android.Support.Design.Widget;
using Android.Views;

namespace AndroMooda3.Listeners
{
    public class Camera2SurfaceTextureListener : Java.Lang.Object, TextureView.ISurfaceTextureListener
    {
        private readonly Camera2Impl camera;
        private readonly MainActivity mainActivity;

        public Camera2SurfaceTextureListener(MainActivity mainActivity, Camera2Impl camera)
        {
            this.camera = camera;
            this.mainActivity = mainActivity;
        }

        public void OnSurfaceTextureAvailable(SurfaceTexture surfaceTexture, int width, int height)
        {
            camera.OpenCamera(new Surface(surfaceTexture));
        }

        public bool OnSurfaceTextureDestroyed(SurfaceTexture surface) => false;
        
        public void OnSurfaceTextureSizeChanged(SurfaceTexture surface, int width, int height)
        {
            Snackbar.Make(mainActivity.rootView, "OnSurfaceTextureSizeChanged", Snackbar.LengthShort).Show();
        }
        
        public void OnSurfaceTextureUpdated(SurfaceTexture surface)
        {
            //updated
        }

    }
}