using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RemixDownloader.Core.Models
{
    public class Board
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("isPrivate")]
        public bool IsPrivate { get; set; }

        [JsonProperty("tags")]
        public List<string> Tags { get; set; }

        [JsonProperty("lastModified")]
        public DateTimeOffset LastModified { get; set; }

        [JsonProperty("creationDate")]
        public DateTimeOffset CreationDate { get; set; }

        [JsonProperty("creator")]
        public Creator Creator { get; set; }

        [JsonProperty("previewImage")]
        public ModelPreviewImage PreviewImage { get; set; }

        [JsonProperty("supplementalImages")]
        public List<ModelPreviewImage> SupplementalImages { get; set; }

        [JsonProperty("creationCount")]
        public long CreationCount { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("permissions")]
        public List<string> Permissions { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("webUri")]
        public string WebUri { get; set; }
    }
}
