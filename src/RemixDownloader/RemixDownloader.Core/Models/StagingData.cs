using Newtonsoft.Json;

namespace RemixDownloader.Core.Models
{
    public class StagingData
    {
        [JsonProperty("stagingDataJson")]
        public string StagingDataJson { get; set; }

        [JsonProperty("viewerStagingData")]
        public ViewerStagingData ViewerStagingData { get; set; }
    }
}
