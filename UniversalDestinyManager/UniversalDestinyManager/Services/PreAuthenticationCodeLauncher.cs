using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UniversalDestinyManager.Models;

namespace UniversalDestinyManager.Services
{
    public static class PreAuthenticationCodeLauncher
    {
        //To-Do handle receiving an Auth Code from a web browser. 

        //Step 1: Check to see if authentication has already happened, in which case ignore this new code.

        //Step 2: Launch the internal authenticator with the new code and other information about the application (client_id)
        public static async Task FinishAuthentication(string code)
        {
            var xForm = new Dictionary<String, String>()
            {
                { "grant_type", "authorization_code" },
                { "client_id", Authenticator.Client_ID},
                { "client_secret", Authenticator.Client_Secret},
                { "code", code}
            };
            var content = new FormUrlEncodedContent(xForm);
            var postRequest = await Authenticator._Web.PostAsync("https://www.bungie.net/platform/app/oauth/token/", content);
            if (postRequest.IsSuccessStatusCode)
            {
                //Everything went well with this authentication
                var accessData = JsonConvert.DeserializeObject<AuthenticationResponse>(await postRequest.Content.ReadAsStringAsync());

                //Set this information in the local storage, cache
                await Authenticator.UpdateSetting("Access_Token", accessData.Access_Token);

                await Authenticator.UpdateSetting("MembershipId", accessData.Membership_Id);
            }
        }

        //Step 3: Send the new authentication information to the Authentication Service
    }
}
