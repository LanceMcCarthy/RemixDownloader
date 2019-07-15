using System.Collections.Generic;
using Newtonsoft.Json;

namespace RemixDownloader.Core.Models
{
    public class BoardResult
    {
        [JsonProperty("results")]
        public List<ModelResult> Results { get; set; }

        [JsonProperty("continuationUri")]
        public string ContinuationUri { get; set; }
    }
}
