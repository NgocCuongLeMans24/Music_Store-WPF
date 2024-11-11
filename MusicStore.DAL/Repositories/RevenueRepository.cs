using Microsoft.EntityFrameworkCore;
using MusicStore.DAL.Models;
using System;
using System.Linq;

namespace MusicStore.DAL.Repositories
{
    public class RevenueRepository
    {
        private readonly MusicStorePrn212Context _context;

        public RevenueRepository()
        {
            _context = new MusicStorePrn212Context(); 
        }

        public (decimal totalRevenue, int totalQuantitySold) GetRevenueReport()
        {
            decimal totalRevenue = _context.Orders
                .Where(o => o.Status != "pending")
                .Sum(o => o.Total);

            int totalQuantitySold = _context.OrderDetails
                .Where(od => od.Order.Status != "pending")
                .Sum(od => od.Quantity);

            return (totalRevenue, totalQuantitySold);
        }
    }
}
