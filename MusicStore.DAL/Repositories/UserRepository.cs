using Microsoft.EntityFrameworkCore;
using MusicStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.DAL.Repositories
{
    public class UserRepository
    {
        private MusicStorePrn212Context _context;
        public User? GetOne(string username, string password)
        {
            _context = new();
            //return _context.Users.ToList(); // tra ve het
            return _context.Users.FirstOrDefault(x => x.Username.Equals(username) && x.Password == password);

        } // hoac tra ve 1 dong, hoac tra ve null, ko thay user hoac pass, sai 1 trong 2 

        public void Update(User u)
        {
            _context = new();
            _context.Update(u);
            _context.SaveChanges();
        }

        public List<User> GetAll()
        {
            _context = new();
            return _context.Users.Include(u => u.Role).ToList();
        }
        public void Add(User u)
        {
            _context = new();
            _context.Add(u);
            _context.SaveChanges();
        }

        public void Delete(User u) { 
            _context = new();
            _context.Users.Remove(u);
            _context.SaveChanges();
        }

        public User CheckUsername(string username)
        {
            _context = new();
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }
        
    }
}
