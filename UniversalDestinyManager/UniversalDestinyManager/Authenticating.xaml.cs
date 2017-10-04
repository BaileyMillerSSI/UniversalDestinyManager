using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
            //Set loading text to Authenticating
            LoadingText.Text = "Authenticating";
            ProgressLoadingWheel.IsActive = true;

            if (e.Parameter != null)
            {
                //Start loading animation
                var code = e.Parameter as String;
                await PreAuthenticationCodeLauncher.FinishAuthentication(code);
                await FinishProfileLoading();
            }
            else
            {
                await FinishProfileLoading();
            }
        }

        private async Task FinishProfileLoading()
        {

            //Set loading text to Loading profile
            LoadingText.Text = "Loading profile";

            //Load Profile
            var profileData = await DestinyApi.GetProfileAsync();


            //Set loading text to Loading platform profile
            LoadingText.Text = "Loading platform profile";

            //Load Platform Profile

            //Available displayNames


            var platformData = await DestinyApi.GetMembershipProfileAsync(profileData.Response.ReturnValidDisplayNames().OrderBy(x => x.Key).Last().Value);

            //Set loading text to Loading character data
            LoadingText.Text = "Loading character data";

            //Loading characters
            var characterData = await DestinyApi.GetCharactersAsync();

            //End loading animation
            if (characterData != null)
            {
                //Find the highest light level character
                var highestLight = characterData.Response.characters.data.OrderByDescending(x => x.Value.light).FirstOrDefault().Value.light;
                LoadingText.Text = $"Welcome {platformData.displayName}! You're highest light character is {highestLight}";
            }
            else
            {
                LoadingText.Text = "";
            }

            ProgressLoadingWheel.IsActive = false;



            //Building Database
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            LoadingText.Margin = new Thickness(0, e.NewSize.Height / 10.8 ,0, 0);
        }
    }
}
