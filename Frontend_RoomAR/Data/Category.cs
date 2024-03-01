using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Frontend_RoomAR.ApplicationData;

public class Category
{
    [JsonPropertyName("categoryId")]
    public int CategoryId { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
}
