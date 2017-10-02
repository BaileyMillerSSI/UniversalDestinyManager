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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UniversalDestinyManager
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AuthenticatingPage : Page
    {
        public AuthenticatingPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            //Start loading animation
            var code = e.Parameter as String;
            await PreAuthenticationCodeLauncher.FinishAuthentication(code);
            //End loading animation

            //After full authentication has finished, load user information for the next few screens

            //Load Profile
            var profileData = await DestinyApi.GetProfileAsync();

            //Load Platform Profile
            var platformData = await DestinyApi.GetMembershipProfileAsync(profileData.Response.xboxDisplayName, Models.PlatformType.TigerXbox);

            //Loading characters
            DestinyApi.GetCharactersAsync(null, Models.PlatformType.TigerXbox);
            //Loading profile stats

            //Building Database
            
        }
    }
}
