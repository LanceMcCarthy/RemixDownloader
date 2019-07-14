using Newtonsoft.Json;

namespace RemixDownloader.Core.Models
{
    public class Application
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
