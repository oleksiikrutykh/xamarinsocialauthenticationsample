using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Facebook.AppEvents;
using Android.Content;
using Xamarin.Facebook;
using Xamarin.Facebook.Login;
using SocialTest.Droid.Renderers;

namespace SocialTest.Droid
{
	[Activity (Label = "SocialTest", Name = "com.oleksiikrutykh.socialtest.MainActivity", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
        private static ICallbackManager callbackManager;

        public static ICallbackManager FacebookCallbackManager
        {
            get
            {
                return callbackManager;
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new SocialTest.App());
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
            FacebookProvider.Instance.TryHandleFacebookActivityResult(requestCode, resultCode, data);
        }



    }
}

