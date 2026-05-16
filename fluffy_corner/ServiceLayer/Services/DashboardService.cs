using fluffy_corner.Data;
using fluffy_corner.ServiceLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace fluffy_corner.ServiceLayer.Services
{
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
            };
            return stats;
        }
    }
}