using MusicStore.DAL.Models;
using MusicStore.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.BLL.Services
{
    public class AlbumService
    {
        private AlbumRepository _repo = new();

        public List<Album> GetAllAlbums()
        {
            return _repo.GetAll();
        }
        public List<Album> GetAlbumsByGenre(int genreId)
        {
            return _repo.GetAllByGenre(genreId);
        }

        public List<Album> GetAlbumsByArtist(int artistId)
        {
            return _repo.GetAllByArtist(artistId);
        }

        public List<Album> SeacrhAlbumByTitle(string search)
        {
            return _repo.SearchAlbum(search);
        }

        public void AddNewAlbum(Album a)
        {
             _repo.Add(a);
        }
         public void UpdateAlbum(Album a)
        {
            _repo.Update(a);
        }

        public void DeleteAlbum(Album a)
        {
            _repo.Delete(a);
        }

        public Album GetAlbumById(int id)
        {
            return _repo.GetById(id);
        }
         public Album CheckTitle(String title)
        {
            return _repo.checkTitle(title);
        }

    }
}
