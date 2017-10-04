using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UniversalDestinyManager.Services;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UniversalDestinyManager
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AuthenticationPage : Page
    {
        public AuthenticationPage()
        {
            this.InitializeComponent();
        }

        private async void LaunchPreAuthWindow_Click(object sender, RoutedEventArgs e)
        {
            var rToken = Authenticator.GetSetting("Refresh_Token");
            if (rToken != null)
            {
                var ReAuthSuccess = await Authenticator.ReAuthenticate();
                if (!ReAuthSuccess)
                {
                    await Authenticator.LaunchPreAuthenticationWindow();
                }
            }
            else {
                await Authenticator.LaunchPreAuthenticationWindow();
            }

            (Window.Current.Content as Frame).Navigate(typeof(AuthenticatingPage));
        }
    }
}
