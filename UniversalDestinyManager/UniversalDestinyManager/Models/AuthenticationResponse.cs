using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversalDestinyManager.Models
{
    public class AuthenticationResponse
    {
        [JsonProperty(PropertyName = "access_token")]
        public string Access_Token { get; set; }

        [JsonProperty(PropertyName = "refresh_token")]
        public string Refresh_Token { get; set; }

        [JsonProperty(PropertyName = "token_type")]
        public string Token_Type { get; set; }

        [JsonProperty(PropertyName = "expires_in")]
        public int Expires_In { get; set; }

        [JsonProperty(PropertyName = "membership_id")]
        public string Membership_Id { get; set; }
    }
}
