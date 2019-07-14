using Newtonsoft.Json;

namespace RemixDownloader.Core.Models
{
    public class ManifestUris
    {
        [JsonProperty("usage")]
        public string Usage { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("format")]
        public string Format { get; set; }
    }
}
