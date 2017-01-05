using Xamarin.Forms;

namespace XamarinTest.Views
{
    public partial class LoginView : ContentPage
    {
        public string OrgType { get; set; }

        public LoginView(string orgType)
        {
            InitializeComponent();
            OrgType = orgType.Contains("Production") ? "login" : "test";
        }
    }
}
