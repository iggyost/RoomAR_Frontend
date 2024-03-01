using Frontend_RoomAR.ApplicationData;
using System;
using System.Collections.Generic;

namespace Frontend_RoomAR.ApplicationData;

public partial class FurnituresCart
{
    public int Id { get; set; }

    public int CartId { get; set; }

    public int FurnitureId { get; set; }

    public int Count { get; set; }

}
