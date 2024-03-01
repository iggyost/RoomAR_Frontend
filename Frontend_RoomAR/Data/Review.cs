using System;
using System.Collections.Generic;

namespace Frontend_RoomAR.ApplicationData;

public partial class Review
{
    public int ReviewId { get; set; }

    public string ReviewText { get; set; } = null!;

    public int UserId { get; set; }

    public int Mark { get; set; }

    public virtual ICollection<FurnituresReview> FurnituresReviews { get; set; } = new List<FurnituresReview>();

    public virtual User User { get; set; } = null!;
}
