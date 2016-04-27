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
using Xamarin.Facebook.Login.Widget;
using Xamarin.Facebook.Login;
using Xamarin.Facebook;
using Java.Lang;
using SocialTest;
using SocialTest.Droid.Renderers;

//[assembly: ExportRenderer(typeof(FacebookLoginPage), typeof( FacebookAndroidRenderer))]

namespace SocialTest.Droid.Renderers
{
    public class FacebookAndroidRenderer : PageRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);
            if (e.NewElement != null)
            {

                //FacebookCallback<LoginResult>
                var loginButton = new LoginButton(this.Context);
                
                var callbackManager = CallbackManagerFactory.Create();

                loginButton.RegisterCallback(callbackManager, new LoginCallback());
                loginButton.SetReadPermissions("user_friends");
                loginButton.SetHeight(100);
                loginButton.SetWidth(100);
                //loginButton.

                this.ViewGroup.AddView(loginButton);
            }
        }


        private class LoginCallback : Java.Lang.Object, IFacebookCallback
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