using System;
using System.Collections.Generic;

namespace Frontend_RoomAR.ApplicationData;

public partial class Photo
{
    public int PhotoId { get; set; }

    public string Photo1 { get; set; } = null!;

    public virtual ICollection<FurnituresPhoto> FurnituresPhotos { get; set; } = new List<FurnituresPhoto>();
}
