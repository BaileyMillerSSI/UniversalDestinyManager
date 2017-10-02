using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
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

namespace UwpUI.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LandingPage : Page
    {
        public LandingPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            FinishAuthenticationAsync(Code.Text = e.Parameter as String);
        }

        private async Task FinishAuthenticationAsync(string code)
        {
            using (var web = new HttpClient())
            {
                var xForm = new Dictionary<String, string>()
                {
                    { "grant_type", "authorization_code"},
                    {"code", code},
                    {"client_id", "" }
                };
                var content = new FormUrlEncodedContent(xForm);
                var rawData = await web.PostAsync("https://www.bungie.net/platform/app/oauth/token/", content);
                var rawStringData =  await rawData.Content.ReadAsStringAsync();
                Code.Text = code;
                AccessToken.Text = JsonConvert.DeserializeObject<AuthenticationCompleteModel>(rawStringData).access_token;

                Clipboard.Clear();

                var clipData = new DataPackage();
                clipData.SetText(AccessToken.Text);
                Clipboard.SetContent(clipData);
            }
        }
    }
}
