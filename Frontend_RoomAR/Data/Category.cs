using System;
using System.Collections.Generic;

namespace Frontend_RoomAR.ApplicationData;

public partial class Category
{
    public int CategoryId { get; set; }

    public string Name { get; set; }

    public virtual ICollection<Furniture> Furnitures { get; set; } = new List<Furniture>();
}
