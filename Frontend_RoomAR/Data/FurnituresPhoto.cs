﻿using System;
using System.Collections.Generic;

namespace Frontend_RoomAR.ApplicationData;

public partial class FurnituresPhoto
{
    public int Id { get; set; }

    public int FurnitureId { get; set; }

    public int PhotoId { get; set; }

    public virtual Furniture Furniture { get; set; } = null!;

    public virtual Photo Photo { get; set; } = null!;
}
