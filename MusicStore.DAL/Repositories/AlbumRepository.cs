using Microsoft.EntityFrameworkCore;
using MusicStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.DAL.Repositories
{

    public class AlbumRepository
    {
        private MusicStorePrn212Context _context;

       
        public List<Album> GetAll()
        {
            _context = new();
            return _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .ToList();
        }
        public List<Album> GetAllByGenre(int genreId)
        {
            _context = new();
            return _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .Where(a => a.GenreId == genreId)
                .ToList();
        }

        public List<Album> GetAllByArtist(int artistId)
        {
            _context = new();
            return _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .Where(a => a.ArtistId == artistId)
                .ToList();
        }

        public List<Album> SearchAlbum(String search)
        {
            _context = new();
            return _context.Albums 
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .Where(a => a.Title.Contains(search)).ToList();
        }

        public void Add(Album a)
        {
            _context = new();
            _context.Albums.Add(a);
            _context.SaveChanges();
        }

        public void Update(Album a)
        {
            _context = new();
            _context.Albums.Update(a);
            _context.SaveChanges();
        }

        public void Delete(Album a)
        {
            _context = new();
            _context.Albums.Remove(a);
            _context.SaveChanges();
        }

        public Album GetById(int id)
        {
            _context = new();
            return _context.Albums.FirstOrDefault(a => a.AlbumId == id);
            
        }

        public Album checkTitle(String title)
        {
            _context = new();
            return _context.Albums.FirstOrDefault(a => a.Title == title);
        }
    }
}
