using Microsoft.EntityFrameworkCore;
using MusicStore.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicStore.DAL.Repositories
{
    public class OrderDetailRepository
    {
        private MusicStorePrn212Context _context;

        public void Add(OrderDetail od)
        {
            _context = new();
            _context.OrderDetails.Add(od);
            _context.SaveChanges();
        }
        public void Update(OrderDetail od)
        {
            _context = new();
            _context.OrderDetails.Update(od);
            _context.SaveChanges();
        }

        public OrderDetail GetByAlbum(Album a, Order o)
        {
            _context = new();
            return _context.OrderDetails.FirstOrDefault(od => od.AlbumId == a.AlbumId && od.OrderId == o.OrderId);
        }

        public List<OrderDetail> GetAll() { 
            _context = new();
            return _context.OrderDetails.Include(od => od.Album).ToList();
        }


        public void Delete(OrderDetail od) { 
            _context = new();
            _context.OrderDetails.Remove(od);
            _context.SaveChanges();
        }

        public List<OrderDetail> GetAllByPendingOrder(Order o)
        {
            _context = new();
            return _context.OrderDetails.Include(od => od.Album).Where(od => od.OrderId == o.OrderId && o.Status == "pending" ).ToList();
        }
        public List<OrderDetail> GetAllByOrder(Order o)
        {
            _context = new();
            return _context.OrderDetails.Include(od => od.Album).Where(od => od.OrderId == o.OrderId).ToList();
        }
    }
}
