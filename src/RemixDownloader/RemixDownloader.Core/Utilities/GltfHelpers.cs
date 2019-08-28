using System;
using System.Linq;
using RemixDownloader.Core.Models;

namespace RemixDownloader.Core.Utilities
{
    public static class GltfHelpers
    {
        public static GltfFile ParseGltfModel(byte[] data)
        {
            var jsonString = System.Text.Encoding.UTF8.GetString(data);

            return GltfFile.FromJson(jsonString);
        }

        /// <summary>
        /// Takes a glTF file URL returned from the API and strips the filename so that "adjacent" components can be downloaded
        /// Credit: @parrotmac
        /// </summary>
        /// <param name="originalGltfUrl"></param>
        /// <returns></returns>
        public static string GetGltfResourceRootUrl(string originalGltfUrl)
        {
            var urlPieces = new Uri(originalGltfUrl);

            var localPath = urlPieces.LocalPath; // e.g. /v3/creations/9...e/gltf/003/9...e/004/0...5/9a1eb96b977d4d11bbca688c9590e11d.glb.gltf

            // Trims off the final component of the path in the URL
            // From /v3/creations/9...e/gltf/003/9...e/004/0...5/9a1eb96b977d4d11bbca688c9590e11d.glb.gltf
            // To   /v3/creations/9...e/gltf/003/9...e/004/0...5
            var pathPieces = localPath.Split('/');

            var sansFinalResource = string.Join("/", pathPieces.SkipLast(1).ToArray());

            return $"{urlPieces.Scheme}://{urlPieces.Host}{sansFinalResource}";
        }
    }
}
