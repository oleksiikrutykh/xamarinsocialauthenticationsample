using SocialTest.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SocialTest
{
	public partial class SocailAuthPage : ContentPage
	{
		public SocailAuthPage ()
		{
			InitializeComponent ();
		}

        private async void AuthenticateFacebookNative(object sender, EventArgs args)
        {
            var res = await FacebookProvider.Instance.Authenticate();
            await this.DisplayAlert("Facebook Native", res.ToString(), "OK");
        }


        private async void AuthenticateFacebook(object sender, EventArgs args)
        {
            var res = await SocialProvider.Instance.AuthenticateFacebook();
            await this.DisplayAlert("Facebook Browser", res.IsAuthenticated.ToString(), "OK");
            res.ToString();
        }

        private async void AuthenticateTwitter(object sender, EventArgs args)
        {
            var res = await SocialProvider.Instance.AuthenticateTwitter();
            await this.DisplayAlert("Twitter Browser", res.IsAuthenticated.ToString(), "OK");
            res.ToString();
        }

        private async void AuthenticateGooglePlus(object sender, EventArgs args)
        {
            var res = await SocialProvider.Instance.AuthenticateGooglePlus();
            await this.DisplayAlert("Google Plus Browser", res.IsAuthenticated.ToString(), "OK");
            res.ToString();
        }

        
    }
}
