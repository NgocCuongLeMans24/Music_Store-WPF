using System;
using System.Collections.Generic;

namespace MusicStore.DAL.Models;

public partial class Artist
{
    public int ArtistId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();
}
