using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Frontend_RoomAR.ApplicationData;

public partial class Photo
{
    [JsonPropertyName("photoId")]
    public int PhotoId { get; set; }
    [JsonPropertyName("photo1")]
    public string Photo1 { get; set; }

}
