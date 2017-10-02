﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UniversalDestinyManager.Models;

namespace UniversalDestinyManager.Services
{
    public static class DestinyApi
    {
        internal static HttpClient _Web = new HttpClient();
        //Now has access to the Api Key and the Access Token
        static DestinyApi()
        {
            _Web.BaseAddress = new Uri("https://www.bungie.net");
            _Web.DefaultRequestHeaders.Add("X-API-KEY", Authenticator.Api_Key);
            _Web.DefaultRequestHeaders.Add("Authorization", $"Bearer {Authenticator.GetSetting("Access_Token")}");
        }

        public static async Task<GetProfileResponse> GetProfileAsync(string membershipId = "")
        {
            if (String.IsNullOrEmpty(membershipId))
            {
                membershipId = Authenticator.GetSetting("MembershipId");
            }
            var rawData = await _Web.GetStringAsync($"Platform/User/GetBungieNetUserById/{membershipId}/");
            var ProfileData = JsonConvert.DeserializeObject<GetProfileResponse>(rawData);
            return ProfileData;
        }

        public static async Task<object> GetMembershipProfileAsync(string displayName, PlatformType membershipType = PlatformType.All)
        {
            var rawData = await _Web.GetStringAsync($"Platform/Destiny2/SearchDestinyPlayer/{(int)membershipType}/{displayName}/");
            return rawData;
        }

        public static async void GetCharactersAsync(string destinyMembershipId = "", PlatformType membershipType = PlatformType.All)
        {
            if (String.IsNullOrEmpty(destinyMembershipId))
            {
                destinyMembershipId = Authenticator.GetSetting("DestinyMembershipId");
            }

            var rawData = await _Web.GetStringAsync($"/Platform/Destiny2/{(int)membershipType}/Profile/{destinyMembershipId}/?components=200");
        }
    }
}