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
using System.Threading.Tasks;
using Xamarin.Facebook.Login;
using Xamarin.Facebook.AppEvents;
using Xamarin.Facebook;
using SocialTest.Droid.Renderers;

namespace SocialTest
{
    public class FacebookProvider
    {
        public static FacebookProvider instance = new FacebookProvider();

        public static FacebookProvider Instance

        {
            get
            {
                return instance;
            }
        }

        private ICallbackManager callbackManager;

        private void EnsureIsInitialized()
        {
            if (callbackManager == null)
            {
                var context = Application.Context;
                Xamarin.Facebook.FacebookSdk.SdkInitialize(context);
                callbackManager = CallbackManagerFactory.Create();
            }
        }

        public Task<bool> Authenticate()
        {
            EnsureIsInitialized();
            Activity currentActivity =  (Activity)Xamarin.Forms.Forms.Context;

            var taskSource = new TaskCompletionSource<bool>();
            FacebookLoginCallback callback = new FacebookLoginCallback();
            callback.AuthenticationCompleted += (s, e) =>
            {
                taskSource.SetResult(e.IsSuccess);
            };

            LoginManager.Instance.RegisterCallback(callbackManager, callback);
            var permissions = new JavaList<string>() { "user_friends" };
            LoginManager.Instance.LogInWithReadPermissions(currentActivity, permissions);

            return taskSource.Task;
        }


        private class FacebookLoginCallback : Java.Lang.Object, IFacebookCallback
        {

            public event EventHandler<AuthenticationCompletedEventArgs> AuthenticationCompleted;

            public void OnCancel()
            {
                var args = new AuthenticationCompletedEventArgs(FacebookResultKind.Cancelled);
                InvokeAuthenticationCompleted(args);
            }

            public void OnError(FacebookException p0)
            {
                var args = new AuthenticationCompletedEventArgs(p0);
                InvokeAuthenticationCompleted(args);
            }

            public void OnSuccess(Java.Lang.Object p0)
            {
                var result = (LoginResult)p0;
                var args = new AuthenticationCompletedEventArgs(result);
                InvokeAuthenticationCompleted(args);
            }

            public void InvokeAuthenticationCompleted(AuthenticationCompletedEventArgs args)
            {
                var handler = this.AuthenticationCompleted;
                if (handler != null)
                {
                    handler.Invoke(this, args);
                }
            }
        }

        public void TryHandleFacebookActivityResult(int requestCode, Result resultCode, Intent data)
        {
            EnsureIsInitialized();
            callbackManager.OnActivityResult(requestCode, (int)resultCode, data);
        }
    }

    public class AuthenticationCompletedEventArgs : EventArgs
    {
        public AuthenticationCompletedEventArgs(LoginResult result)
            : this(FacebookResultKind.Success, result, null)
        {
        }

        public AuthenticationCompletedEventArgs(FacebookException exception)
            : this(FacebookResultKind.Error, null, exception)
        {
        }

        public AuthenticationCompletedEventArgs(FacebookResultKind kind)
            : this(kind, null, null)
        {
        }

        public AuthenticationCompletedEventArgs(FacebookResultKind kind, LoginResult result, FacebookException exception)
        {
            this.Result = result;
            this.Exception = exception;
            this.ResultKind = kind;
        }

        public LoginResult Result { get; private set; }

        public FacebookException Exception { get; private set; }

        public FacebookResultKind ResultKind { get; private set; }

        public bool IsSuccess
        {
            get
            {
                return this.ResultKind == FacebookResultKind.Success;
            }
        }
    }

    public enum FacebookResultKind
    {
        Success,
        Error,
        Cancelled
    }
}