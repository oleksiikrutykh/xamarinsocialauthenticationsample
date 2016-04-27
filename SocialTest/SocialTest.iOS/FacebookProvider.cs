using Facebook.LoginKit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UIKit;

namespace SocialTest
{
    public class FacebookProvider
    {
        public static FacebookProvider instance = new FacebookProvider();

        private LoginManager loginManager = new LoginManager();

        public static FacebookProvider Instance
        {
            get
            {
                return instance;
            }
        }

        public async Task<bool> Authenticate()
        {
            var permissions = new string[] { "user_friends" };
            UIViewController rootViewController = UIApplication.SharedApplication.KeyWindow.RootViewController;
            var result = await loginManager.LogInWithReadPermissionsAsync(permissions, rootViewController);
            bool isSuccess = result.Token != null;
            return isSuccess;
        }
    }
}
