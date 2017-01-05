using System;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinTest.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XamarinTest
{
    public class App : Application
    {
        private static Account _userAccount;
        public static Account UserAccount => _userAccount;

        public static bool IsLoggedIn => UserAccount != null;

        public App()
        {
            MainPage = new NavigationPage(new MainView());
        }

        public static void SaveAccount(Account account)
        {
            _userAccount = account;
        }

        public static Action SuccessfulLoginAction
        {
            get
            {
                return async () =>
                {
                    //_navPage.Navigation.PopModalAsync();
                    await Current.MainPage.Navigation.PopModalAsync();
                    await Current.MainPage.Navigation.PushAsync(new ProfileView());

                };
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
