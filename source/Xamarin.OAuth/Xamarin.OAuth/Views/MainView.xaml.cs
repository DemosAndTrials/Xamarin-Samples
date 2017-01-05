using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using XamarinTest.ViewModels;

namespace XamarinTest.Views
{
    public partial class MainView : ContentPage
    {
        public MainView()
        {
            InitializeComponent();
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            var btn = sender as Button;

            await Navigation.PushModalAsync(new LoginView(btn.Text));
        }
    }
}
