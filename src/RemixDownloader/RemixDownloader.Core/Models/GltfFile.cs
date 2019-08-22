using System.Globalization;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RemixDownloader.Core.Models
{
    public class GltfFile
    {
        [JsonProperty("buffers")]
        public List<GltfBuffer> Buffers { get; set; }

        [JsonProperty("images")]
        public List<GltfImage> Images { get; set; }

        public static GltfFile FromJson(string json) => JsonConvert.DeserializeObject<GltfFile>(json, new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        });
    }
}
