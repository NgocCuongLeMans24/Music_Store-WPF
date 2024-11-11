using System;
using System.Collections.Generic;

namespace MusicStore.DAL.Models;

public partial class Cart
{
    public int RecordId { get; set; }

    public string CartId { get; set; } = null!;

    public int OrderDetailId { get; set; }

    public int Count { get; set; }

    public DateTime DateCreated { get; set; }

    public virtual OrderDetail OrderDetail { get; set; } = null!;
}
