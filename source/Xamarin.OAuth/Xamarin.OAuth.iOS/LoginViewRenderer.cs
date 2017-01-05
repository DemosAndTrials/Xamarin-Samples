
using System;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Xamarin.OAuth.Shared;
using XamarinTest.iOS;
using XamarinTest.Views;

[assembly: ExportRenderer(typeof(LoginView), typeof(LoginViewRenderer))]
namespace XamarinTest.iOS
{
    class LoginViewRenderer : PageRenderer
    {
        private bool _isShown;

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            if (_isShown) return;
            _isShown = true;
            
            var org = (Element as LoginView)?.OrgType;
            // Initialize the object that communicates with the OAuth service
            var auth = new OAuth2Authenticator(
              Constants.ClientId,
              Constants.Scope,
              new Uri(string.Format(Constants.AuthorizeUrl, org)),
              new Uri(Constants.CallbackUrl));

            // Register an event handler for when the authentication process completes
            auth.Completed += OnAuthenticationCompleted;

            // Display the UI
            PresentViewController(auth.GetUI(), true, null);
        }

        async void OnAuthenticationCompleted(object sender, AuthenticatorCompletedEventArgs e)
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