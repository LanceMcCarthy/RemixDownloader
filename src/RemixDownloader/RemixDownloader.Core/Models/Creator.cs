using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RemixDownloader.Core.Models
{
    public class Creator
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("profileImageUri")]
        public string ProfileImageUri { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("joinedDate")]
        public DateTimeOffset JoinedDate { get; set; }

        [JsonProperty("permissions")]
        public List<string> Permissions { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("webUri")]
        public string WebUri { get; set; }
    }
}
