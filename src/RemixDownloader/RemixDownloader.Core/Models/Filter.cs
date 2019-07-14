using System.Collections.Generic;
using Newtonsoft.Json;

namespace RemixDownloader.Core.Models
{
    public class Filter
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("allowedValues", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> AllowedValues { get; set; }
    }
}
