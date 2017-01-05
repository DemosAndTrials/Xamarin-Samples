using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using XamarinTest.ViewModels;

namespace XamarinTest.Views
{
    public partial class ProfileView : ContentPage
    {
        public ProfileViewModel DefaultViewModel
        {
            get { return new ProfileViewModel(); }
        }

        public ProfileView()
        {
            InitializeComponent();
            BindingContext = DefaultViewModel;
        }
    }
}
