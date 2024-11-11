using MusicStore.DAL.Models;
using MusicStore.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.BLL.Services
{
    
    public class OrderService
    {
        private OrderRepository _repo = new();

        public Order FindPendingOrder(int userId)
        {
            return _repo.FindPendingOrder(userId);
        }
        public void AddNewOrder(Order o) { 
            _repo.Add(o);
        }
        public void UpdateOrder(Order o)
        {
            _repo.Update(o);
        }

        public List<Order> GetAllOrdersByUserId(int id) { 
           return _repo.GetAllByUserId(id);
        }
    }
}
