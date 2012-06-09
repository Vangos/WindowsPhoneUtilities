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
using Microsoft.Phone.Controls;
using WindowsPhone.Utilities;

namespace WindowsPhone.Utilities.Test
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            tblManufacturer.Text += Device.Properties.Manufacturer;
            tblDeviceID.Text += Device.Properties.DeviceID;
            tblWindowsLive.Text += Device.Properties.WindowsLiveAnonymousID;
            tblAppVersion.Text += Device.Properties.ApplicationVersion;
            tblInternetConnectivity.Text += Device.Properties.HasInternetConnectivity;
            tblSessionID.Text += Device.Properties.SessionID;
        }

        private void SaveString_Click(object sender, RoutedEventArgs e)
        {
            if (txtResult.Text != string.Empty)
            {
                Storage.Settings.Save("foo", txtResult.Text);
            }
        }

        private void SaveInteger_Click(object sender, RoutedEventArgs e)
        {
            if (txtResult.Text != string.Empty)
            {
                Storage.Settings.Save("foo", int.Parse(txtResult.Text));
            }
        }

        private void LoadString_Click(object sender, RoutedEventArgs e)
        {
            string result = Storage.Settings.Load<string>("foo");

            MessageBox.Show(result != null ? result : "null");
        }

        private void LoadInteger_Click(object sender, RoutedEventArgs e)
        {
            int result = Storage.Settings.Load<int>("foo");

            MessageBox.Show(result.ToString());
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Storage.Settings.Delete("foo");
        }
    }
}