using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;

namespace PDFDeSecureDroid
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        public Action<int, Result, Intent?>? Callback;
        protected override void OnActivityResult(int requestCode, Result resultCode, Intent? data)
        {
            if (Callback != null)
            {
                Callback(requestCode, resultCode, data);
            }
        }

    }
}
