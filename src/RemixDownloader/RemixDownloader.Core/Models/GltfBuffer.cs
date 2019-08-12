
using Newtonsoft.Json;

namespace RemixDownloader.Core.Models
{
    public class GltfBuffer
    {
        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("byteLength")]
        public int ByteLength { get; set; }
    }
}
