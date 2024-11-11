using System;
using System.Collections.Generic;

namespace MusicStore.DAL.Models;

public partial class Genre
{
    public int GenreId { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();
}
