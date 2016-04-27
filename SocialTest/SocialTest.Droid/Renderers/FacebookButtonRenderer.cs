using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using SocialTest;
using SocialTest.Droid.Renderers;
using Xamarin.Facebook;
using Xamarin.Facebook.Login.Widget;
using Xamarin.Facebook.Login;

//[assembly: ExportRenderer(typeof(FacebookButton), typeof(FacebookButtonRenderer))]

namespace SocialTest.Droid.Renderers
{
    public class FacebookButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {
                var loginButton = new LoginButton(this.Context);
                loginButton.RegisterCallback(MainActivity.FacebookCallbackManager, new FacebookLoginCallback());
                loginButton.SetReadPermissions("user_friends");
                this.SetNativeControl(loginButton);
            }
        }

        private class FacebookLoginCallback : Java.Lang.Object, IFacebookCallback
        {

            public void OnCancel()
            {

            }

            public void OnError(FacebookException p0)
            {

            }

            public void OnSuccess(Java.Lang.Object p0)
            {
                var result = (LoginResult)p0;

            }
        }

    }
}