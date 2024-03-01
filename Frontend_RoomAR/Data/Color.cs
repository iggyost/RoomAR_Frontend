using System;
using System.Collections.Generic;

namespace Frontend_RoomAR.ApplicationData;
public partial class Color
{
    public int ColorId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<FurnituresColor> FurnituresColors { get; set; } = new List<FurnituresColor>();
}
