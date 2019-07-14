using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RemixDownloader.Core.Models
{
    public class ModelResult
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("manifestUris")]
        public List<ManifestUris> ManifestUris { get; set; }

        [JsonProperty("assetUris")]
        public List<AssetUris> AssetUris { get; set; }

        [JsonProperty("creator")]
        public Creator Creator { get; set; }

        [JsonProperty("tags")]
        public List<string> Tags { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("creationDate")]
        public DateTimeOffset CreationDate { get; set; }

        [JsonProperty("lastModifiedDate")]
        public DateTimeOffset LastModifiedDate { get; set; }

        [JsonProperty("previewImage")]
        public ModelPreviewImage PreviewImage { get; set; }

        [JsonProperty("previewSpriteSheet")]
        public ModelPreviewImage PreviewSpriteSheet { get; set; }

        [JsonProperty("application")]
        public Application Application { get; set; }

        [JsonProperty("stagingData")]
        public StagingData StagingData { get; set; }

        [JsonProperty("previewImageStagingData")]
        public StagingData PreviewImageStagingData { get; set; }

        [JsonProperty("permissions")]
        public List<string> Permissions { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("fileSize")]
        public long FileSize { get; set; }

        [JsonProperty("triangleCount")]
        public long TriangleCount { get; set; }

        [JsonProperty("webUri")]
        public string WebUri { get; set; }

        [JsonProperty("hasAnimations")]
        public bool HasAnimations { get; set; }
    }
}
