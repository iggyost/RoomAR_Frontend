using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Frontend_RoomAR.ApplicationData;

public class Furniture
{
    [JsonPropertyName("furnitureId")]
    public int FurnitureId { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("cost")]
    public decimal Cost { get; set; }
    [JsonPropertyName("description")]
    public string Description { get; set; }
    [JsonPropertyName("coverPhoto")]
    public string CoverPhoto { get; set; }
    [JsonPropertyName("width")]
    public double Width { get; set; }
    [JsonPropertyName("height")]
    public double Height { get; set; }
    [JsonPropertyName("length")]
    public double Length { get; set; }
    [JsonPropertyName("weight")]
    public double Weight { get; set; }

    public int CategoryId { get; set; }


}
