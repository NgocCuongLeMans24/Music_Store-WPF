using MusicStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.DAL.Repositories
{
    public class OrderRepository
    {
        private MusicStorePrn212Context _context;

        public Order FindPendingOrder(int userId)
        {
            _context = new();
            return _context.Orders.FirstOrDefault(o => o.Userid == userId && o.Status == "pending");
        }

        public void Add(Order o)
        {
            _context = new();
            _context.Orders.Add(o);
            _context.SaveChanges();
        }

        public void Update(Order o)
        {
            _context = new();
            _context.Orders.Update(o);
            _context.SaveChanges();
        }

        public List<Order> GetAllByUserId(int id) { 
            _context = new();
            return  _context.Orders.Where(o => o.Userid==id).ToList();
        }
    }
}
