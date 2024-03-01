using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Frontend_RoomAR.ApplicationData;

public partial class ViewUserCart
{
    public string Name { get; set; } = null!;

    public decimal Cost { get; set; }

    public string? CoverPhoto { get; set; }

    public int FurnitureId { get; set; }

    public int CartId { get; set; }

    public int UserId { get; set; }

    public int Count { get; set; }
    public decimal? TotalCost { get; set; }
    public int Id { get; set; }
}
