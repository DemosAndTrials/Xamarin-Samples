using System;
using Android.App;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.OAuth.Shared;
using XamarinTest.Droid;
using XamarinTest.Views;

[assembly: ExportRenderer(typeof(LoginView), typeof(LoginViewRenderer))]
namespace XamarinTest.Droid
{
    class LoginViewRenderer : PageRenderer
    {
        private bool _isShown;

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            if (_isShown) return;
            _isShown = true;

            var org = (e.NewElement as LoginView)?.OrgType;

            var auth = new OAuth2Authenticator(
                Constants.ClientId,
                Constants.Scope,
                new Uri(string.Format(Constants.AuthorizeUrl, org)),
                new Uri(Constants.CallbackUrl));
            auth.Completed += OnAuthenticationCompleted;

            // Display the UI
            var activity = Context as Activity;
            activity?.StartActivity(auth.GetUI(activity));
        }

        private async void OnAuthenticationCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            if (e.IsAuthenticated)
            {
                await LoginHelper.FetchUserInfo(e.Account);
                App.SaveAccount(e.Account);
                App.SuccessfulLoginAction();
            }
        }
    }
}