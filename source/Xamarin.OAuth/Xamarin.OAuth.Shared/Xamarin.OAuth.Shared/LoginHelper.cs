using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Auth;

namespace Xamarin.OAuth.Shared
{
    public static class LoginHelper
    {
        public const string AppName = "XamarinFormsOAuth";

        public static async Task FetchUserInfo(Account account)
        {
            var request = new OAuth2Request("GET", new Uri(account.Properties["id"]), null, account);
            var response = await request.GetResponseAsync(); 
            if (response != null)
            {
                var userJson = response.GetResponseText();
                var user = JObject.Parse(userJson);
                account.Username = (string)user[Constants.UsernameAccountProperty];
                account.Properties[Constants.EmailAccountProperty] = (string)user[Constants.EmailAccountProperty];
                account.Properties[Constants.PhotoAccountProperty] = (string)user[Constants.PhotoAccountProperty]["picture"];


                try
                {
                    AccountStore.Create().Save(account, AppName);
                }
                catch (Exception e)
                {

                }
                UpdateViewModel(account);
            }

            //await Navigator.Current.AuthenticationComplete();
        }

        // alternative call
        private static async Task FetchAccount(Account account)
        {
            try
            {
                var httpClient = new HttpClient();
                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, new Uri(Constants.UserInfoUrl));
                message.Headers.Add("Authorization", "OAuth " + account.Properties["access_token"]);
                message.Headers.Add("ContentType", "application/json");
                var resp = await httpClient.SendAsync(message);
                var sr = new StreamReader(await resp.Content.ReadAsStreamAsync());
                string response = sr.ReadToEnd().Trim();
                var jo = JObject.Parse(response);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private static void UpdateViewModel(Account account)
        {
            //Locator.MainViewModel.EmailAddress = account.Properties[Constants.EmailAccountProperty];
            //Locator.MainViewModel.PhotoUrl = account.Properties[Constants.PhotoAccountProperty];
        }
    }
}
