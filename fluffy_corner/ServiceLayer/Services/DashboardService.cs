using fluffy_corner.ServiceLayer.Interfaces;
using fluffy_corner.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace fluffy_corner.ServiceLayer.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _context;

        public DashboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<string, int>> GetAdminDashboardStatsAsync()
        {
            var stats = new Dictionary<string, int>
            {
                { "TotalUsers", await _context.Users.CountAsync() },
                { "TotalProducts", await _context.Products.CountAsync() },
                { "TotalOrders", await _context.Orders.CountAsync() },
                { "TotalCategories", await _context.Categories.CountAsync() }
            };
            return stats;
        }
    }
}