using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RemixDownloader.Core.Models
{
    public class RemixUserResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("profileImageUri")]
        public Uri ProfileImageUri { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("joinedDate")]
        public DateTimeOffset JoinedDate { get; set; }

        [JsonProperty("permissions")]
        public List<object> Permissions { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("publicBoardCount")]
        public long PublicBoardCount { get; set; }

        [JsonProperty("creationCount")]
        public long CreationCount { get; set; }

        [JsonProperty("webUri")]
        public Uri WebUri { get; set; }

        public static RemixUserResponse FromJson(string json) => JsonConvert.DeserializeObject<RemixUserResponse>(json, new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        });
    }

    // Example response
    // {
    // "id":"46reU3-wFPz",
    // "profileImageUri":"https://remixservice.azureedge.net/ms-user-profile-images/MS Logomark.png",
    // "username":"Microsoft",
    // "joinedDate":"2016-09-08T18:04:38Z",
    // "permissions":[],
    // "state":"Active",
    // "publicBoardCount":204,
    // "creationCount":2139,
    // "webUri":"https://www.remix3d.com/user/46reU3-wFPz"
    // }
}
