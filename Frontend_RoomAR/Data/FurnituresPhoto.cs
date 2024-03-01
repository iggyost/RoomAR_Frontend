using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Frontend_RoomAR.ApplicationData;

public partial class FurnituresPhoto
{
    public int Id { get; set; }
    [JsonPropertyName("furnitureId")]
    public int FurnitureId { get; set; }
    [JsonPropertyName("photoId")]
    public int PhotoId { get; set; }

    public virtual Furniture Furniture { get; set; }

    //public virtual Photo Photo { get; set; }
}
