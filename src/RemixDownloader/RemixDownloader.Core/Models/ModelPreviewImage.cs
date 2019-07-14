using Newtonsoft.Json;

namespace RemixDownloader.Core.Models
{
    public class ModelPreviewImage
    {
        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("originalWidth")]
        public double OriginalWidth { get; set; }

        [JsonProperty("originalHeight")]
        public double OriginalHeight { get; set; }

        [JsonProperty("previewWidth")]
        public double PreviewWidth { get; set; }

        [JsonProperty("previewHeight")]
        public double PreviewHeight { get; set; }
    }
}
