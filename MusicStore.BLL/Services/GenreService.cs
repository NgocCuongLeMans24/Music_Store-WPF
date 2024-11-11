using MusicStore.DAL.Models;
using MusicStore.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.BLL.Services
{
    public class GenreService
    {
        private GenreRepository _repo = new();

        public List<Genre> GetAllGenres()
        {
            return _repo.GetAll();
        }
    }
}
