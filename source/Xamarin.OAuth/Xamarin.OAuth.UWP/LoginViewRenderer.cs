using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Windows.Security.Authentication.Web;
using Xamarin.Auth;
using Xamarin.Forms.Platform.UWP;
using Xamarin.OAuth.Shared;
using XamarinTest.UWP;
using XamarinTest.Views;

[assembly: ExportRenderer(typeof(LoginView), typeof(LoginViewRenderer))]
namespace XamarinTest.UWP
{
    class LoginViewRenderer : PageRenderer
    {
        private bool _isShown;

        protected override async void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (_isShown) return;
            _isShown = true;

            var org = (sender as LoginView)?.OrgType;
            var code = await AuthenticateUsingWebAuthenticationBroker(org);
            if (!string.IsNullOrEmpty(code))
            {
                var account = ConvertCodeToAccount(code);
                await LoginHelper.FetchUserInfo(account);
                XamarinTest.App.SaveAccount(account);
                XamarinTest.App.SuccessfulLoginAction();
            }
        }

        private async Task<string> AuthenticateUsingWebAuthenticationBroker(string org)
        {
            var sfdcUrl = string.Format(Constants.AuthorizeUrl, org) + "?client_id=" +
                            Uri.EscapeDataString(Constants.ClientId);
            sfdcUrl += "&display=page";
            sfdcUrl += "&redirect_uri=" + Uri.EscapeDataString(Constants.CallbackUrl);
            sfdcUrl += "&response_type=token";
            sfdcUrl += "&scope=" + Uri.EscapeDataString(Constants.Scope);

            var startUri = new Uri(sfdcUrl);

            var webAuthenticationResult =
              await
                WebAuthenticationBroker.AuthenticateAsync(WebAuthenticationOptions.None, startUri,
                  new Uri(Constants.CallbackUrl));
            return webAuthenticationResult.ResponseStatus != WebAuthenticationStatus.Success ? null : webAuthenticationResult.ResponseData;
        }

        private static Account ConvertCodeToAccount(string code)
        {
            var responseUri = new Uri(code);
            string[] parameters = responseUri.Fragment.Substring(1).Split('&');

            Dictionary<string, string> values = new Dictionary<string, string>();
            foreach (var parameter in parameters)
            {
                string[] parts = parameter.Split('=');
                string name = Uri.UnescapeDataString(parts[0]);
                string value = parts.Length > 1 ? Uri.UnescapeDataString(parts[1]) : "";
                values.Add(name, value);
            }
            return new Account(null, values);
        }
    }
}
