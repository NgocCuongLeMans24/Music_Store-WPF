using MusicStore.DAL.Models;
using MusicStore.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.BLL.Services
{
    public class ArtistService
    {
        private ArtistRepository _repo = new();

        public List<Artist> GetAllArtists()
        {
            return _repo.GetAll();
        }
    }

    
}
