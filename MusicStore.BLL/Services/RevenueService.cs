using MusicStore.DAL.Repositories;
using System;

namespace MusicStore.BLL.Services
{
    public class RevenueService
    {
        private RevenueRepository _repo;

        public RevenueService()
        {
            _repo = new RevenueRepository();
        }

        public (decimal totalRevenue, int totalQuantitySold) GetRevenueReport()
        {
            return _repo.GetRevenueReport();
        }
    }
}
