using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace RemixDownloader.Core.Models
{
    public class RemixBoardListResponse
    {
        [JsonProperty("totalCount")]
        public long TotalCount { get; set; }

        [JsonProperty("results")]
        public List<BoardResult> Results { get; set; }

        [JsonProperty("continuationUri")]
        public string ContinuationUri { get; set; }

        public static RemixBoardListResponse FromJson(string json) => JsonConvert.DeserializeObject<RemixBoardListResponse>(json, new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        });
    }

    // **** Example Response ***** //
    //
    /*
     *
     * {
    "totalCount": 28,
    "results": [{
        "board": {
            "id": "3vkCqsxjqeH",
            "name": "Get Started in Paint 3D",
            "description": "Use the models in this board to explore the 3D editing tools in Paint 3D. \r\n\r\nUpload your finished creations back to Remix 3D and tag them with #MadeWithPaint3D!",
            "isPrivate": false,
            "tags": ["paint"],
            "lastModified": "2018-06-15T20:14:30Z",
            "creationDate": "2017-07-06T21:46:11Z",
            "creator": {
                "id": "46reU3-wFPz",
                "profileImageUri": "https://remixservice.azureedge.net/ms-user-profile-images/MS Logomark.png",
                "username": "Microsoft",
                "joinedDate": "2016-09-08T18:04:38Z",
                "permissions": ["Report"],
                "state": "Active",
                "webUri": "https://www.remix3d.com/user/46reU3-wFPz"
            },
            "previewImage": {
                "source": "https://encoding.assets.remix3d.com:443/003/0ce93dec1a8e43f6b262faff8b34015f/001/08586725131056667658993017976cu01/1bcf147c00434753830430db58e93c1b?w=308&h=308&format=jpg",
                "originalWidth": 308,
                "originalHeight": 308,
                "previewWidth": 308,
                "previewHeight": 308
            },
            "supplementalImages": [{
                "source": "https://encoding.assets.remix3d.com:443/003/0ce93dec1a8e43f6b262faff8b34015f/001/08586725131056667658993017976cu01/1bcf147c00434753830430db58e93c1b?w=308&h=308&format=jpg",
                "originalWidth": 308,
                "originalHeight": 308,
                "previewWidth": 308,
                "previewHeight": 308
            }],
            "creationCount": 32,
            "type": "Normal",
            "permissions": ["Like", "Export", "Report"],
            "state": "Active",
            "webUri": "https://www.remix3d.com/board/3vkCqsxjqeH"
        },
        "type": "Board"
    }, {
        "board": {
            "id": "3sbfVZRbeJu",
            "name": "Latest 3D collections",
            "description": "Whether you are looking for a model to use in your next PowerPoint presentation, for something to edit in Paint 3D, add a flourish to your Photos, or something to view in your real world with Mixed Reality Viewer, we've got tons of models to get you started.\r\n \r\nThis board is updated regularly with new collections, so check back often!",
            "isPrivate": false,
            "tags": ["3D"],
            "lastModified": "2018-06-15T19:52:37Z",
            "creationDate": "2017-09-22T18:54:28Z",
            "creator": {
                "id": "46reU3-wFPz",
                "profileImageUri": "https://remixservice.azureedge.net/ms-user-profile-images/MS Logomark.png",
                "username": "Microsoft",
                "joinedDate": "2016-09-08T18:04:38Z",
                "permissions": ["Report"],
                "state": "Active",
                "webUri": "https://www.remix3d.com/user/46reU3-wFPz"
            },
            "previewImage": {
                "source": "https://encoding.assets.remix3d.com:443/003/253ec0d89efa41768b6bfae609c9f707/001/08586725236262746465311780011cu38/5b2e103b09914d22bd90ea469679e7c4?w=308&h=308&format=jpg",
                "originalWidth": 308,
                "originalHeight": 308,
                "previewWidth": 308,
                "previewHeight": 308
            },
            "supplementalImages": [{
                "source": "https://encoding.assets.remix3d.com:443/003/253ec0d89efa41768b6bfae609c9f707/001/08586725236262746465311780011cu38/5b2e103b09914d22bd90ea469679e7c4?w=308&h=308&format=jpg",
                "originalWidth": 308,
                "originalHeight": 308,
                "previewWidth": 308,
                "previewHeight": 308
            }],
            "creationCount": 85,
            "type": "Normal",
            "permissions": ["Like", "Export", "Report"],
            "state": "Active",
            "webUri": "https://www.remix3d.com/board/3sbfVZRbeJu"
        },
        "type": "Board"
    }, {
        "board": {
            "id": "3w-8Vj8gUzU",
            "name": "Animals",
            "description": "A collection of realistic animals.",
            "isPrivate": false,
            "tags": ["animal", "Office 3D"],
            "lastModified": "2018-06-14T17:24:45Z",
            "creationDate": "2017-06-30T17:16:20Z",
            "creator": {
                "id": "46reU3-wFPz",
                "profileImageUri": "https://remixservice.azureedge.net/ms-user-profile-images/MS Logomark.png",
                "username": "Microsoft",
                "joinedDate": "2016-09-08T18:04:38Z",
                "permissions": ["Report"],
                "state": "Active",
                "webUri": "https://www.remix3d.com/user/46reU3-wFPz"
            },
            "previewImage": {
                "source": "https://encoding.assets.remix3d.com:443/003/0000000900000000000c172a00000000/001/08586726900096264770725606949cu34/a843d6e23613499ab17196c9b8a39c40?w=308&h=308&format=jpg",
                "originalWidth": 308,
                "originalHeight": 308,
                "previewWidth": 308,
                "previewHeight": 308
            },
            "supplementalImages": [{
                "source": "https://encoding.assets.remix3d.com:443/003/0000000900000000000c172a00000000/001/08586726900096264770725606949cu34/a843d6e23613499ab17196c9b8a39c40?w=308&h=308&format=jpg",
                "originalWidth": 308,
                "originalHeight": 308,
                "previewWidth": 308,
                "previewHeight": 308
            }],
            "creationCount": 57,
            "type": "Normal",
            "permissions": ["Like", "Export", "Report"],
            "state": "Active",
            "webUri": "https://www.remix3d.com/board/3w-8Vj8gUzU"
        },
        "type": "Board"
    }, {
        "board": {
            "id": "3x9fT-punE1",
            "name": "Space",
            "description": "A collection of realistic planets, moons, stars and space crafts.",
            "isPrivate": false,
            "tags": ["astronaut", "Office 3D"],
            "lastModified": "2018-06-14T17:22:08Z",
            "creationDate": "2017-06-01T18:35:50Z",
            "creator": {
                "id": "46reU3-wFPz",
                "profileImageUri": "https://remixservice.azureedge.net/ms-user-profile-images/MS Logomark.png",
                "username": "Microsoft",
                "joinedDate": "2016-09-08T18:04:38Z",
                "permissions": ["Report"],
                "state": "Active",
                "webUri": "https://www.remix3d.com/user/46reU3-wFPz"
            },
            "previewImage": {
                "source": "https://encoding.assets.remix3d.com:443/003/000000090000000000ed162a00000000/001/08586748407891250497412607610/63d1a5048f8b46d88c5085f77fb1050b?w=308&h=308&format=jpg",
                "originalWidth": 308,
                "originalHeight": 308,
                "previewWidth": 308,
                "previewHeight": 308
            },
            "supplementalImages": [{
                "source": "https://encoding.assets.remix3d.com:443/003/000000090000000000ed162a00000000/001/08586748407891250497412607610/63d1a5048f8b46d88c5085f77fb1050b?w=308&h=308&format=jpg",
                "originalWidth": 308,
                "originalHeight": 308,
                "previewWidth": 308,
                "previewHeight": 308
            }],
            "creationCount": 18,
            "type": "Normal",
            "permissions": ["Like", "Export", "Report"],
            "state": "Active",
            "webUri": "https://www.remix3d.com/board/3x9fT-punE1"
        },
        "type": "Board"
    }, {
        "board": {
            "id": "3x9fVXp9NgB",
            "name": "Dinosaurs",
            "description": "A collection of realistic dinosaurs.",
            "isPrivate": false,
            "tags": ["Office 3D"],
            "lastModified": "2018-08-22T17:33:30Z",
            "creationDate": "2017-06-01T18:35:30Z",
            "creator": {
                "id": "46reU3-wFPz",
                "profileImageUri": "https://remixservice.azureedge.net/ms-user-profile-images/MS Logomark.png",
                "username": "Microsoft",
                "joinedDate": "2016-09-08T18:04:38Z",
                "permissions": ["Report"],
                "state": "Active",
                "webUri": "https://www.remix3d.com/user/46reU3-wFPz"
            },
            "previewImage": {
                "source": "https://encoding.assets.remix3d.com:443/003/000000090000000000c9591e00000000/001/08586745858402164542621332272/dc771e5d2c7a4482bb9d7f5409850d1e?w=308&h=308&format=jpg",
                "originalWidth": 308,
                "originalHeight": 308,
                "previewWidth": 308,
                "previewHeight": 308
            },
            "supplementalImages": [{
                "source": "https://encoding.assets.remix3d.com:443/003/000000090000000000c9591e00000000/001/08586745858402164542621332272/dc771e5d2c7a4482bb9d7f5409850d1e?w=308&h=308&format=jpg",
                "originalWidth": 308,
                "originalHeight": 308,
                "previewWidth": 308,
                "previewHeight": 308
            }],
            "creationCount": 18,
            "type": "Normal",
            "permissions": ["Like", "Export", "Report"],
            "state": "Active",
            "webUri": "https://www.remix3d.com/board/3x9fVXp9NgB"
        },
        "type": "Board"
    }, {
        "board": {
            "id": "3uaE7cyRYWT",
            "name": "Vinyl Toy",
            "description": "A collection of models in the vinyl-toy art style.",
            "isPrivate": false,
            "tags": ["vinyl-toy"],
            "lastModified": "2018-06-14T17:25:26Z",
            "creationDate": "2017-08-04T15:18:49Z",
            "creator": {
                "id": "46reU3-wFPz",
                "profileImageUri": "https://remixservice.azureedge.net/ms-user-profile-images/MS Logomark.png",
                "username": "Microsoft",
                "joinedDate": "2016-09-08T18:04:38Z",
                "permissions": ["Report"],
                "state": "Active",
                "webUri": "https://www.remix3d.com/user/46reU3-wFPz"
            },
            "previewImage": {
                "source": "https://encoding.assets.remix3d.com:443/003/00000009000000008001830300000000/001/08586745858683402666296624705/422281bea5614dbaab964128b46fbf28?w=308&h=308&format=jpg",
                "originalWidth": 308,
                "originalHeight": 308,
                "previewWidth": 308,
                "previewHeight": 308
            },
            "supplementalImages": [{
                "source": "https://encoding.assets.remix3d.com:443/003/00000009000000008001830300000000/001/08586745858683402666296624705/422281bea5614dbaab964128b46fbf28?w=308&h=308&format=jpg",
                "originalWidth": 308,
                "originalHeight": 308,
                "previewWidth": 308,
                "previewHeight": 308
            }],
            "creationCount": 92,
            "type": "Normal",
            "permissions": ["Like", "Export", "Report"],
            "state": "Active",
            "webUri": "https://www.remix3d.com/board/3uaE7cyRYWT"
        },
        "type": "Board"
    }, {
        "board": {
            "id": "3uaE6W1Rdsl",
            "name": "Realistic",
            "description": "A collection of realistic-styled models.",
            "isPrivate": false,
            "tags": ["realistic"],
            "lastModified": "2018-06-14T17:25:19Z",
            "creationDate": "2017-08-04T15:19:01Z",
            "creator": {
                "id": "46reU3-wFPz",
                "profileImageUri": "https://remixservice.azureedge.net/ms-user-profile-images/MS Logomark.png",
                "username": "Microsoft",
                "joinedDate": "2016-09-08T18:04:38Z",
                "permissions": ["Report"],
                "state": "Active",
                "webUri": "https://www.remix3d.com/user/46reU3-wFPz"
            },
            "previewImage": {
                "source": "https://encoding.assets.remix3d.com:443/003/0000000900000000002e172a00000000/001/08586748081011774406537242805/c6fbb142f14e4f418623ed828acffeb1?w=308&h=308&format=jpg",
                "originalWidth": 308,
                "originalHeight": 308,
                "previewWidth": 308,
                "previewHeight": 308
            },
            "supplementalImages": [{
                "source": "https://encoding.assets.remix3d.com:443/003/0000000900000000002e172a00000000/001/08586748081011774406537242805/c6fbb142f14e4f418623ed828acffeb1?w=308&h=308&format=jpg",
                "originalWidth": 308,
                "originalHeight": 308,
                "previewWidth": 308,
                "previewHeight": 308
            }],
            "creationCount": 118,
            "type": "Normal",
            "permissions": ["Like", "Export", "Report"],
            "state": "Active",
            "webUri": "https://www.remix3d.com/board/3uaE6W1Rdsl"
        },
        "type": "Board"
    }, {
        "board": {
            "id": "3w-84cNk7AR",
            "name": "Flowers and Plants",
            "description": "A collection of realistic plants and flowers.",
            "isPrivate": false,
            "tags": ["nature", "Office 3D"],
            "lastModified": "2018-08-08T16:23:35Z",
            "creationDate": "2017-06-30T17:20:02Z",
            "creator": {
                "id": "46reU3-wFPz",
                "profileImageUri": "https://remixservice.azureedge.net/ms-user-profile-images/MS Logomark.png",
                "username": "Microsoft",
                "joinedDate": "2016-09-08T18:04:38Z",
                "permissions": ["Report"],
                "state": "Active",
                "webUri": "https://www.remix3d.com/user/46reU3-wFPz"
            },
            "previewImage": {
                "source": "https://encoding.assets.remix3d.com:443/003/000000090000000080d17a2a00000000/001/08586748405339779887364338458/c41db7cb57fb4ca6af3575a78321dd26?w=308&h=308&format=jpg",
                "originalWidth": 308,
                "originalHeight": 308,
                "previewWidth": 308,
                "previewHeight": 308
            },
            "supplementalImages": [{
                "source": "https://encoding.assets.remix3d.com:443/003/000000090000000080d17a2a00000000/001/08586748405339779887364338458/c41db7cb57fb4ca6af3575a78321dd26?w=308&h=308&format=jpg",
                "originalWidth": 308,
                "originalHeight": 308,
                "previewWidth": 308,
                "previewHeight": 308
            }],
            "creationCount": 18,
            "type": "Normal",
            "permissions": ["Like", "Export", "Report"],
            "state": "Active",
            "webUri": "https://www.remix3d.com/board/3w-84cNk7AR"
        },
        "type": "Board"
    }, {
        "board": {
            "id": "3s2TamZQ-7d",
            "name": "Best of Minecraft",
            "description": "Here are some of our favorite Minecraft models exported to Remix3D.com.\r\n\r\nLearn more about the export feature in Minecraft: https://aka.ms/remix3D_minecraft",
            "isPrivate": false,
            "tags": ["Minecraft"],
            "lastModified": "2018-06-14T17:26:33Z",
            "creationDate": "2017-10-06T20:19:53Z",
            "creator": {
                "id": "46reU3-wFPz",
                "profileImageUri": "https://remixservice.azureedge.net/ms-user-profile-images/MS Logomark.png",
                "username": "Microsoft",
                "joinedDate": "2016-09-08T18:04:38Z",
                "permissions": ["Report"],
                "state": "Active",
                "webUri": "https://www.remix3d.com/user/46reU3-wFPz"
            },
            "previewImage": {
                "source": "https://encoding.assets.remix3d.com:443/003/000000090000000080b86a2d00000000/001/08586775127436692781722802983/864a3d540d204057959f62791adb1319?w=308&h=308&format=jpg",
                "originalWidth": 308,
                "originalHeight": 308,
                "previewWidth": 308,
                "previewHeight": 308
            },
            "supplementalImages": [{
                "source": "https://encoding.assets.remix3d.com:443/003/000000090000000080b86a2d00000000/001/08586775127436692781722802983/864a3d540d204057959f62791adb1319?w=308&h=308&format=jpg",
                "originalWidth": 308,
                "originalHeight": 308,
                "previewWidth": 308,
                "previewHeight": 308
            }],
            "creationCount": 16,
            "type": "Normal",
            "permissions": ["Like", "Export", "Report"],
            "state": "Active",
            "webUri": "https://www.remix3d.com/board/3s2TamZQ-7d"
        },
        "type": "Board"
    }, {
        "board": {
            "id": "46reQcWKKqd",
            "name": "Cool Cars",
            "description": "These are the snazziest cars on the lot. Give one a test drive!",
            "isPrivate": false,
            "tags": ["car", "vehicle"],
            "lastModified": "2018-06-14T17:20:08Z",
            "creationDate": "2016-09-08T18:05:07Z",
            "creator": {
                "id": "46reU3-wFPz",
                "profileImageUri": "https://remixservice.azureedge.net/ms-user-profile-images/MS Logomark.png",
                "username": "Microsoft",
                "joinedDate": "2016-09-08T18:04:38Z",
                "permissions": ["Report"],
                "state": "Active",
                "webUri": "https://www.remix3d.com/user/46reU3-wFPz"
            },
            "previewImage": {
                "source": "https://encoding.assets.remix3d.com:443/003/000000090000000000bf820300000000/001/08586748520228687851695225315/229cc53c0e1e45e4987fd787174e89a1?w=308&h=308&format=jpg",
                "originalWidth": 308,
                "originalHeight": 308,
                "previewWidth": 308,
                "previewHeight": 308
            },
            "supplementalImages": [{
                "source": "https://encoding.assets.remix3d.com:443/003/000000090000000000bf820300000000/001/08586748520228687851695225315/229cc53c0e1e45e4987fd787174e89a1?w=308&h=308&format=jpg",
                "originalWidth": 308,
                "originalHeight": 308,
                "previewWidth": 308,
                "previewHeight": 308
            }],
            "creationCount": 28,
            "type": "Normal",
            "permissions": ["Like", "Export", "Report"],
            "state": "Active",
            "webUri": "https://www.remix3d.com/board/46reQcWKKqd"
        },
        "type": "Board"
    }],
    "continuationUri": "https://api.remix3d.com:443/v3/channels/34b78f58881242e4ab611e4ab5ffaa78/items?%24continuationToken=eyJTa2lwQ291bnQiOjEwfQ2"
}
     *
     */
}
