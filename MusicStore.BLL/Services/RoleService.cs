using MusicStore.DAL.Models;
using MusicStore.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.BLL.Services
{

    public class RoleService
    {
        private RoleRepository _repo = new();
        public List<Role> GetAllRoles()
        {
           return _repo.GetAll();
        }
    }
}
