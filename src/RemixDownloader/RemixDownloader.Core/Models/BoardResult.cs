using Newtonsoft.Json;

namespace RemixDownloader.Core.Models
{
    public class BoardResult
    {
        [JsonProperty("board")]
        public Board Board { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
