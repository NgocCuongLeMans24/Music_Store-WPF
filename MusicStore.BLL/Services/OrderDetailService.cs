using MusicStore.DAL.Models;
using MusicStore.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.BLL.Services
{
    public class OrderDetailService
    {
        private OrderDetailRepository _repo = new();
        public void AddOrderDetail(OrderDetail od)
        {
            _repo.Add(od);
        }

        public void UpdateOrderDetail(OrderDetail od) { 
            _repo.Update(od);
        }

        public OrderDetail GetOrderDetailByAlbum(Album a, Order o) { 
            return _repo.GetByAlbum(a,o);
        }

        public List<OrderDetail> GetAllOrderDetails()
        {
            return _repo.GetAll();
        }

        public void DeleteOrderDetail(OrderDetail od) { 
            _repo.Delete(od);
        }

        public List<OrderDetail> GetAllDetailByPendingOrder(Order o)
        {
            return _repo.GetAllByPendingOrder(o);
        }

        public List<OrderDetail> GetDetailAllByOrder(Order o)
        {
            return _repo.GetAllByOrder(o);
        }
    }
}
