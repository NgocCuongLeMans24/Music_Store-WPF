using MusicStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.DAL.Repositories
{

    public class GenreRepository
    {
        private MusicStorePrn212Context _context;

        public List<Genre> GetAll()
        {
            _context = new();
            return _context.Genres.ToList();
        }
    }
}
