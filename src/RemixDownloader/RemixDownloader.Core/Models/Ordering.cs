using Newtonsoft.Json;

namespace RemixDownloader.Core.Models
{
    public class Ordering
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("direction")]
        public string Direction { get; set; }
    }
}
