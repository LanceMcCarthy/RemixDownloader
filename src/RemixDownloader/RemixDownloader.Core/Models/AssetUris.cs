using Newtonsoft.Json;

namespace RemixDownloader.Core.Models
{
    public class AssetUris
    {
        [JsonProperty("optimizationType")]
        public string OptimizationType { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("format")]
        public string Format { get; set; }

        [JsonProperty("version")]
        public double Version { get; set; }
    }
}
