using MusicStore.DAL.Models;
using MusicStore.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.BLL.Services
{
    public class UserService
    {
        private UserRepository _repo = new();
        public User? Authenticate(string username, string password)
        {
            return _repo.GetOne(username, password);
        }

        public void UpdateUser(User u)
        {
            _repo.Update(u);
        }

        public List<User> GetAllUsers() { return _repo.GetAll(); }

        public void AddUser(User u) { _repo.Add(u); }

        public void DeleteUser(User u) { _repo.Delete(u); }

        public User CheckUserame(string username)
        {
            return _repo.CheckUsername(username);
        }
    }
}
