using MusicStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.DAL.Repositories
{
    public class ArtistRepository
    {
        private MusicStorePrn212Context _context;

        public List<Artist> GetAll()
        {
            _context = new();
            return _context.Artists.ToList();
        }
    }
}
