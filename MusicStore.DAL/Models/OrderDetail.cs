using System;
using System.Collections.Generic;

namespace MusicStore.DAL.Models;

public partial class OrderDetail
{
    public int OrderDetailId { get; set; }

    public int OrderId { get; set; }

    public int AlbumId { get; set; }

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public virtual Album Album { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
