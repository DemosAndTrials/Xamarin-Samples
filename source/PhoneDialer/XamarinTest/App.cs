using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinTest.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XamarinTest
{
    public class App : Application
    {
        public App()
        {
            //Button btnYes = new Button
            //{
            //    Text = "I am a button",
            //    HorizontalOptions = LayoutOptions.Center
            //};
            //btnYes.Clicked += (sender, args) =>
            //{
            //    btnYes.Text = "You clicked button";
            //};
            //Button btnNo = new Button
            //{
            //    Text = "I am a button",
            //    HorizontalOptions = LayoutOptions.Center
            //};
            //btnYes.Clicked += (sender, args) =>
            //{
            //    btnYes.Text = "You clicked button";
            //};

            //// The root page of your application
            //var content = new ContentPage
            //{
            //    Title = "XamarinTest",
            //    Content = new StackLayout
            //    {
            //        VerticalOptions = LayoutOptions.Center,
            //        Children = {
            //            new Label {
            //                HorizontalTextAlignment = TextAlignment.Center,
            //                Text = "Welcome to Xamarin Forms!"
            //            },
            //            btnYes
            //        }
            //    }
            //};

            MainPage = new MainView();
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
