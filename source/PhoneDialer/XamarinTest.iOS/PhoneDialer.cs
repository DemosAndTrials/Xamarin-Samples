using Foundation;
using UIKit;
using Xamarin.Forms;
using XamarinTest.Abstract;
using XamarinTest.iOS;

[assembly: Dependency(typeof(PhoneDialer))]
namespace XamarinTest.iOS
{
    public class PhoneDialer : IDialer
    {
        public bool Dial(string number)
        {
            return UIApplication.SharedApplication.OpenUrl(
                new NSUrl("tel:" + number));
        }
    }
}