using MusicStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.DAL.Repositories
{
    public class RoleRepository
    {
        private MusicStorePrn212Context _context;
        public List<Role> GetAll()
        {
            _context = new();
            return _context.Roles.ToList();
        }
    }
}
