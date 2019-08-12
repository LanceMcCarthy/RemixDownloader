
using Newtonsoft.Json;

namespace RemixDownloader.Core.Models
{
    public class GltfImage
    {
        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("mimeType")]
        public string MimeType { get; set; }
    }
}
