
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Auth;

#if __ANDROID__

using Android.App;
using Android.Content;

#endif

#if __IOS__
using UIKit;
#endif

namespace SocialTest.Providers
{
    public class SocialProvider
    {
        private static SocialProvider instance = new SocialProvider();


        public static SocialProvider Instance
        {
            get
            {
                return instance;
            }
        }

        public Task<AuthenticatorCompletedEventArgs> AuthenticateFacebook()
        {
            var auth = new OAuth2Authenticator(clientId: "1728764067337217",
                                               scope: "",
                                               authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth/"),
                                               redirectUrl: new Uri("http://www.facebook.com/connect/login_success.html"));
            return Authenticate(auth);
        }

        public Task<AuthenticatorCompletedEventArgs> AuthenticateTwitter()
        {
            var auth = new OAuth1Authenticator("OZMkOkjFSkX0P3HHlyrrZ6zCY",
                                               "CBBaG1StwdLnexsTg7elKfz2GTTXlUOSzTrVghnLDX3MUGvP5Z",
                                               new Uri("https://api.twitter.com/oauth/request_token"),
                                               new Uri("https://api.twitter.com/oauth/authorize"),
                                               new Uri("https://api.twitter.com/oauth/access_token"),
                                               new Uri("http://www.facebook.com/connect/login_success.html")
                                               );
            return Authenticate(auth);
        }

        public Task<AuthenticatorCompletedEventArgs> AuthenticateGooglePlus()
        {
            var auth = new OAuth2Authenticator(clientId: "14897113860-227glp78tfs9uieb8ccgo6ge14ihsfc3.apps.googleusercontent.com",
                                               clientSecret: "LYcEOsrCdVD8LFFRMz95Mjk6",
                                               scope: "openid",
                                               authorizeUrl: new Uri("https://accounts.google.com/o/oauth2/auth"),
                                               accessTokenUrl: new Uri("https://accounts.google.com/o/oauth2/token"),
                                               redirectUrl: new Uri("http://www.facebook.com/connect/login_success.html"));
            return Authenticate(auth);
        }

        private Task<AuthenticatorCompletedEventArgs> Authenticate(Authenticator authenticator)
        {
            var taskSource = new TaskCompletionSource<AuthenticatorCompletedEventArgs>();

#if __ANDROID__
            authenticator.Completed += (s, e) =>
            {
                var account = e.Account;
                taskSource.SetResult(e);
            };

            Activity activity = (Activity)Xamarin.Forms.Forms.Context;
            Intent authIntent = authenticator.GetUI(Application.Context);
            activity.StartActivity(authIntent);
#endif

#if __IOS__

            UIViewController rootViewController = UIApplication.SharedApplication.KeyWindow.RootViewController;
            

            var authenticationViewController = authenticator.GetUI();
            authenticator.Completed += (s, e) =>
            {
                rootViewController.DismissViewController(true, null);
                var account = e.Account;
                taskSource.SetResult(e);
            };

            rootViewController.PresentViewController(authenticationViewController, true, null);
#endif

            return taskSource.Task;
        }

        
    }


    
}
