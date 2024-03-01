﻿using System;
using System.Collections.Generic;

namespace Backend_RoomAr.ApplicationData;

public partial class User
{
    public int UserId { get; set; }

    public string? Name { get; set; }
    public string? Surname { get; set; }

    public string? PhoneNum { get; set; }

    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
