﻿using System;
using System.Collections.Generic;

namespace Frontend_RoomAR.ApplicationData;

public partial class User
{
    public int UserId { get; set; }

    public string Name { get; set; }
    public string Surname { get; set; }

    public string PhoneNum { get; set; }

    public string Email { get; set; }
    public string Password { get; set; }

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
}
