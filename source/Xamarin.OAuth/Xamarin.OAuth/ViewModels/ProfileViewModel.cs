using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.OAuth.Shared;

namespace XamarinTest.ViewModels
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                if (_username == value) return;
                _username = value;
                OnPropertyChanged();
            }
        }

        private string _emailAddress;
        public string EmailAddress
        {
            get { return _emailAddress; }
            set
            {
                if (_emailAddress == value) return;
                _emailAddress = value;
                OnPropertyChanged();
            }
        }

        private string _photoUrl;
        public string PhotoUrl
        {
            get { return _photoUrl; }
            set
            {
                if (_photoUrl == value) return;
                _photoUrl = value;
                OnPropertyChanged();
            }
        }

        public ProfileViewModel()
        {
            if (App.UserAccount != null)
            {
                Username = App.UserAccount.Username;
                EmailAddress = App.UserAccount.Properties[Constants.EmailAccountProperty];
                PhotoUrl = App.UserAccount.Properties[Constants.PhotoAccountProperty];
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
