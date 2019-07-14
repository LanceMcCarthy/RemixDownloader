using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace RemixDownloader.Core.Services
{
    public class ModelConverterApiService
    {
        private const string ApiRoot = "https://tools3d.azurewebsites.net/api";
        private const string ModelConverter = "/convert?creationid=G009SX0LPVF1&datasilo=live&environment=release&cv=PGc9eYrgaGuTs6HN4tgqhs.15";
        private readonly HttpClient client;

        public ModelConverterApiService()
        {
            var handler = new HttpClientHandler();

            if (handler.SupportsAutomaticDecompression)
            {
                handler.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            }

            client = new HttpClient(handler);
        }

        /// <summary>
        /// NOT READY
        /// </summary>
        /// <param name="creationId"></param>
        /// <returns></returns>
        public async Task<byte[]> ConvertAsync(string creationId)
        {
            var response = await client.GetByteArrayAsync($"{ApiRoot}/convert?creationid={creationId}&datasilo=live&environment=release");

            return response;
        }

        // Example convert request
        // tools3d.azurewebsites.net/api/convert?creationid=G009SX0LPVF1&datasilo=live&environment=release&cv=PGc9eYrgaGuTs6HN4tgqhs.15

        // It's usually preceded with a launch protocol for a local app, like all these launched from Edge Canary

        // 3D Builder (when selecting "Print")
        // IMPORTANT! - Notice this one requires a conversion from a different API endpoint
        // com.microsoft.builder3d://tools3d.azurewebsites.net/api/convert?creationid=G009SX0LPVF1&datasilo=live&environment=release&cv=PGc9eYrgaGuTs6HN4tgqhs.15

        // Paint3D
        // ms-paint://edit?id=G009SX0LPVF1&source=remix3d&browserName=Chrome&browserVersion=77.0.3843.0&cv=PGc9eYrgaGuTs6HN4tgqhs.19

        // Windows 10 Mixed Reality app, passes a model ID directly to the app (must use local library)
        // armodelviewing://model/?remixId=G009SX0LPVF1&cv=PGc9eYrgaGuTs6HN4tgqhs.22&browserName=Chrome

    }
}
