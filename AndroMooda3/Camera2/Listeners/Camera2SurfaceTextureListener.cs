using Android.Graphics;
using Android.Support.Design.Widget;
using Android.Views;

namespace AndroMooda3.Listeners
{
    public class Camera2SurfaceTextureListener : Java.Lang.Object, TextureView.ISurfaceTextureListener
    {
        private readonly CameraActivity mainActivity;
        public delegate void SurfaceMethod(Surface surface);
        private event SurfaceMethod surfaceMethod;

        /// <param name="mainActivity"></param>
        /// <param name="surfaceMethod"> Method that has a surface as a parameter. </param>
        public Camera2SurfaceTextureListener(CameraActivity mainActivity, SurfaceMethod surfaceMethod = null)
        {
            this.mainActivity = mainActivity;
            this.surfaceMethod += surfaceMethod;
        }

        public void OnSurfaceTextureAvailable(SurfaceTexture surfaceTexture, int width, int height)
        {
            surfaceMethod?.Invoke(new Surface(surfaceTexture));
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