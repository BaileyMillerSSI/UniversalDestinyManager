using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
            return localSettings.Values[key].ToString();
        }

        internal static T GetSetting<T>(string key)
        {
            var setting = GetSetting(key);
            return (T)Convert.ChangeType(setting, typeof(T));
        }
    }
}
