using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Facebook.Client;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace SleepHack
{
    public partial class FacebookLoginPage : PhoneApplicationPage
    {
        public FacebookLoginPage()
        {
            InitializeComponent();
            Loaded += FacebookLoginPageLoaded;
        }

        async void FacebookLoginPageLoaded(object sender, RoutedEventArgs e)
        {
            if (!App.isAuthenticated)
            {
                App.isAuthenticated = true;
                await Authenticate();
            }
        }

        private FacebookSession _session;
        public async Task Authenticate()
        {

            try
            {
                _session = await App.FacebookSessionClient.LoginAsync("user_about_me,read_stream");
                App.AccessToken = _session.AccessToken;
                App.FacebookId = _session.FacebookId;

                Dispatcher.BeginInvoke(() => NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative)));
            }
            catch (InvalidOperationException e)
            {
                var message = "Login failed! Exception details: " + e.Message;
                MessageBox.Show(message);
            }
        }
    }
}