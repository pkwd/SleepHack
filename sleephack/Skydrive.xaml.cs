using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Live;
using Microsoft.Live.Controls;
using Microsoft.Phone.Controls;

namespace SleepHack
{
    public partial class Skydrive : PhoneApplicationPage
    {
        public Skydrive()
        {
            InitializeComponent();
        }
        public void btnShowContent_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement this method
            throw new NotImplementedException();
        }

        private void btnSignin_SessionChanged(object sender, LiveConnectSessionChangedEventArgs e)
        {
            if (e.Status == LiveConnectSessionStatus.Connected)
            {
                //   client = new LiveConnectClient(e.Session);
                //   App.Current.LiveSession = e.Session;
                this.txtLoginResult.Text = "Signed in.";
                this.txtWelcome.Visibility = System.Windows.Visibility.Visible;
                this.btnShowContent.Visibility = System.Windows.Visibility.Visible;
                this.btnAddFile.Visibility = System.Windows.Visibility.Visible;

                //client.GetCompleted += new EventHandler<LiveOperationCompletedEventArgs>(OnGetCompleted);
                // client.GetAsync("me", null);
            }
            else
            {
                this.txtLoginResult.Text = "Not signed in.";
                this.txtWelcome.Visibility = System.Windows.Visibility.Collapsed;
                //  client = null;
            }
        }

        void OnGetCompleted(object sender, LiveOperationCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                if (e.Result.ContainsKey("first_name") &&
                    e.Result.ContainsKey("last_name"))
                {
                    if (e.Result["first_name"] != null &&
                        e.Result["last_name"] != null)
                    {
                        this.txtWelcome.Text =
                            "Hello, " +
                            e.Result["first_name"].ToString() + " " +
                            e.Result["last_name"].ToString() + "!";
                    }
                }
                else
                {
                    txtWelcome.Text = "Hello, signed-in user!";
                }
            }
            else
            {
                txtWelcome.Text = "Error calling API: " +
                    e.Error.ToString();
            }
        }

        private void btnAddFile_Click(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}