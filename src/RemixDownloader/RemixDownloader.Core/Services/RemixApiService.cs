using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using RemixDownloader.Core.Models;

namespace RemixDownloader.Core.Services
{
    public class RemixApiService : IDisposable
    {
        private static RemixApiService current;
        public static RemixApiService Current => current ?? (current = new RemixApiService());

        private readonly HttpClient client;
        private const string ApiRoot = "https://api.remix3d.com/v3";
        
        public RemixApiService()
        {
            var handler = new HttpClientHandler();

            if (handler.SupportsAutomaticDecompression)
            {
                handler.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            }

            client = new HttpClient(handler);
        }

        public async Task<RemixUserListResponse> GetModelsForUserAsync(string userId, string continuationUrl = null)
        {
            string json = string.Empty;

            if (string.IsNullOrEmpty(continuationUrl))
            {
                // If starting from scratch for a new user
                json = await client.GetStringAsync($"{ApiRoot}/users/{userId}/uploads");
            }
            else
            {
                // If this is a continuation, there is a continuation token already prepared for us.
                // ex. "continuationUri": "hXXp://api.remix3d.com:443/v3/users/xxxxxxxx/uploads?%24continuationToken=xxxxxxxxxx."
                json = await client.GetStringAsync(continuationUrl);
            }
            
            return RemixUserListResponse.FromJson(json);
        }


        ///  <summary>
        ///  There are several ways to get the model, depends on what quality and file type you want. See the example response in RemixUserListResponse.cs class for full details.
        /// 
        ///  "manifestUris": [{
        ///     "usage": "View",
        ///     "uri": "https://api.remix3d.com:443/v3/creations/G009SX0LPN59/gltf/...",
        ///     "format": "GLTF"
        /// }, {
        ///     "usage": "Download",
        ///     "uri": "https://api.remix3d.com:443/v3/creations/G009SX0LPN59/gltf/...",
        ///     "format": "GLTF"
        /// }],
        /// "assetUris": [{
        ///     "optimizationType": "Preview",
        ///     "uri": "https://api.remix3d.com:443/v3/creations/G009SX0LPN59/assets/preview/gltf/1/...",
        ///     "format": "GLTF",
        ///     "version": 1
        /// }, {
        ///     "optimizationType": "Performance",
        ///     "uri": "https://api.remix3d.com:443/v3/creations/G009SX0LPN59/assets/performance/gltf/1/...",
        ///     "format": "GLTF",
        ///     "version": 1
        /// }, {
        ///     "optimizationType": "Quality",
        ///     "uri": "https://api.remix3d.com:443/v3/creations/G009SX0LPN59/assets/quality/gltf/1/...",
        ///     "format": "GLTF",
        ///     "version": 1
        /// }, {
        ///     "optimizationType": "HoloLens",
        ///     "uri": "https://api.remix3d.com:443/v3/creations/G009SX0LPN59/assets/hololens/glb/1/...",
        ///     "format": "GLB",
        ///     "version": 1
        /// }, {
        ///     "optimizationType": "WindowsMR",
        ///     "uri": "...",
        ///     "format": "GLB",
        ///     "version": 1
        /// }]
        ///  </summary>
        ///  <param name="model">Model to get file for</param>
        ///  <param name="levelOfDetail">Level of detail requested in the downloaded model</param>
        ///  <returns></returns>
        public async Task<byte[]> DownloadModelFilesAsync(ModelResult model, AssetOptimizationType levelOfDetail = AssetOptimizationType.OriginalDownload)
        {
            string downloadUrl = string.Empty;

            if (levelOfDetail == AssetOptimizationType.OriginalView)
            {
                downloadUrl = model.ManifestUris.FirstOrDefault(u => u.Usage == "View")?.Uri;
            }
            else if (levelOfDetail == AssetOptimizationType.OriginalDownload)
            {
                downloadUrl = model.ManifestUris.FirstOrDefault(u => u.Usage == "Download")?.Uri;
            }
            else
            {
                downloadUrl = model.AssetUris.FirstOrDefault(u => u.OptimizationType == levelOfDetail.ToString())?.Uri;
            }

            if (string.IsNullOrEmpty(downloadUrl))
            {
                return null;
            }

            var response = await client.GetByteArrayAsync(downloadUrl);

            return response;
        }

        public void Dispose()
        {
            client?.Dispose();
        }
    }
}

