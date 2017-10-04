using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UniversalDestinyManager.Models;
using Windows.System;

namespace UniversalDestinyManager.Services
{
    public static class Authenticator
    {
        internal static String Client_ID = "21086";
        internal static String Client_Secret = "WyBhHmAtIZrvUP6WeZt7G2GjViWgx6sUueRd6DeplAA";
        internal static String Api_Key = "831dfc2968db40a2bfd1ec9e297a7991";
        internal static HttpClient _Web = new HttpClient();
        internal static bool RetrievedApiKey
        {
            get
            {
                return Authenticator._Web.DefaultRequestHeaders.Contains("X-API-KEY");
            }
        }

        static Authenticator()
        {
            _Web.DefaultRequestHeaders.Add("X-API-KEY", Authenticator.Api_Key);
        }

        public static async Task LaunchPreAuthenticationWindow()
        {
            const string AuthorizationPath = "https://www.bungie.net/en/OAuth/Authorize?client_id={0}&response_type=code";

            await Launcher.LaunchUriAsync(new Uri(String.Format(AuthorizationPath, Authenticator.Client_ID)));
        }

        public static async Task<bool> ReAuthenticate()
        {
            var xForm = new Dictionary<String, String>()
            {
                { "grant_type", "refresh_token" },
                { "client_id", Authenticator.Client_ID},
                { "client_secret", Authenticator.Client_Secret},
                { "refresh_token", Authenticator.GetSetting("Refresh_Token")}
            };
            var content = new FormUrlEncodedContent(xForm);
            var postRequest = await Authenticator._Web.PostAsync("https://www.bungie.net/platform/app/oauth/token/", content);
            if (postRequest.IsSuccessStatusCode)
            {
                //Everything went well with this authentication
                var accessData = JsonConvert.DeserializeObject<AuthenticationResponse>(await postRequest.Content.ReadAsStringAsync());

                //Set this information in the local storage, cache
                await Authenticator.UpdateSetting("Access_Token", accessData.Access_Token);
                await Authenticator.UpdateSetting("Refresh_Token", accessData.Refresh_Token);
                await Authenticator.UpdateSetting("Token_Expires", accessData.Expires_In.ToString());

                await Authenticator.UpdateSetting("MembershipId", accessData.Membership_Id);
            }

            return postRequest.IsSuccessStatusCode;
        }

        internal static async Task UpdateSetting(string key, string value)
        {
            await Task.Factory.StartNew(() => 
            {
                Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

                localSettings.Values[key] = value;
            });
        }

        internal static string GetSetting(string key)
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            if (localSettings.Values.ContainsKey(key))
            {
                return localSettings.Values[key].ToString();
            }
            else {
                return null;
            }
        }

        internal static object GetSettingRaw(string key)
        {
            Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            if (localSettings.Values.ContainsKey(key))
            {
                return localSettings.Values[key];
            }
            else
            {
                return null;
            }
        }

        internal static T GetSetting<T>(string key)
        {
            var setting = GetSettingRaw(key);
            return (T)Convert.ChangeType(setting, typeof(T));
        }
    }
}
