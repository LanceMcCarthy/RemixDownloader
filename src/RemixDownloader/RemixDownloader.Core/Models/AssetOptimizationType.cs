using System;
using System.ComponentModel;

namespace RemixDownloader.Core.Models
{
    [Flags]
    public enum AssetOptimizationType
    {
        [Description("Preview")]
        Preview = 1,
        [Description("Performance")]
        Performance = 2,
        [DefaultValue(true)]
        [Description("Quality")]
        Quality = 4,
        [Description("HoloLens")]
        HoloLens = 8,
        [Description("Windows Mixed Reality")]
        WindowsMR = 16,
        [Description("Web Preview")]
        ManifestView = 32,
        [Description("Original Download")]
        ManifestDownload = 64,
    }
}
