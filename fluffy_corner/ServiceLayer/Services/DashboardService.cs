using fluffy_corner.Data;
using fluffy_corner.ServiceLayer.DTOs;
using fluffy_corner.ServiceLayer.Interfaces;
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
                { "TotalCategories", await _context.Categories.CountAsync() },
                { "TotalOrders", await _context.Orders.CountAsync() }
            };

            return stats;
        }

        public async Task<List<SalesDataDto>> GetSalesAnalyticsAsync()
        {
            // to get sales data for the last 7 days,
            // we can use LINQ to group orders by date and
            // count the total amount for each day

            var lastWeek = DateTime.Now.AddDays(-7);

            return await _context.Orders
                .Where(o => o.OrderDate >= lastWeek)
                .GroupBy(o => o.OrderDate.Date)
                .Select(g => new SalesDataDto
                {
                    Date = g.Key.ToString("ddd"), 
                    TotalAmount = g.Count(),
                })
                .ToListAsync();
        }


    } 
} 

