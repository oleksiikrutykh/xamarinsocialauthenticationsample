using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Facebook.CoreKit;

namespace SocialTest.iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
        // Replace here you own Facebook App Id and App Name, if you don't have one go to
        // https://developers.facebook.com/apps
        string appId = "718522901623097";
        string appName = "Test­X­a­m­a­i­nIos";

        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
            Profile.EnableUpdatesOnAccessTokenChange(true);
            Settings.AppID = appId;
            Settings.DisplayName = appName;

            global::Xamarin.Forms.Forms.Init ();
			LoadApplication (new SocialTest.App ());

			return ApplicationDelegate.SharedInstance.FinishedLaunching(app, options);
        }

        public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
        {
            // We need to handle URLs by passing them to their own OpenUrl in order to make the SSO authentication works.
            return ApplicationDelegate.SharedInstance.OpenUrl(application, url, sourceApplication, annotation);
        }

        public override void OnActivated(UIApplication application)
        {
            // Call the 'ActivateApp' method to log an app event for use
            // in analytics and advertising reporting. This is optional
            AppEvents.ActivateApp();
        }
    }
}
