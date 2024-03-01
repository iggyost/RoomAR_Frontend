using Frontend_RoomAR.ApplicationData;
using System;
using System.Collections.Generic;

namespace Frontend_RoomAR.ApplicationData;

public partial class Cart
{
    public int CartId { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<FurnituresCart> FurnituresCarts { get; set; } = new List<FurnituresCart>();

}
