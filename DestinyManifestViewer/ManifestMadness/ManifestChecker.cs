using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ManifestMadness
{
    public static class ManifestChecker
    {
        private static HttpClient _Web = new HttpClient();

        private static String _ApiKey = "";

        public static void SetApiKey(string key)
        {
            _ApiKey = key;
        }

        public static GetManifestResponse CheckManifestVersion()
        {
            return CheckManifestVersionAsync().Result;
        }

        public static async Task<GetManifestResponse> CheckManifestVersionAsync()
        {
            AppendApiKey();

            var jsonResponse = await _Web.GetStringAsync("https://www.bungie.net/Platform/Destiny2/Manifest/");

            var data = JsonConvert.DeserializeObject<GetManifestResponse>(jsonResponse);
            
            return data;

        }



        private static void AppendApiKey()
        {
            RemoveApiKey();
            _Web.DefaultRequestHeaders.Add("X-API-KEY", _ApiKey);
        }

        private static void RemoveApiKey(string name = "X-API-KEY")
        {
            _Web.DefaultRequestHeaders.Remove(name);
        }
    }
}
