using System;
using System.Collections.Generic;

namespace MusicStore.DAL.Models;

public partial class Album
{
    public int AlbumId { get; set; }

    public int GenreId { get; set; }

    public int ArtistId { get; set; }

    public string Title { get; set; } = null!;

    public decimal Price { get; set; }

    public string AlbumUrl { get; set; } = null!;

    public int Stock { get; set; }

    public bool? IsTop10BestSeller { get; set; }

    public virtual Artist Artist { get; set; } = null!;

    public virtual Genre Genre { get; set; } = null!;

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
