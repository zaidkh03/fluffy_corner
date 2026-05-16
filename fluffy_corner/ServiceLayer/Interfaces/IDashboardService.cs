using fluffy_corner.ServiceLayer.DTOs;

namespace fluffy_corner.ServiceLayer.Interfaces
{
    public interface IDashboardService
    {
        Task<Dictionary<string, int>> GetAdminDashboardStatsAsync();
        Task<List<SalesDataDto>> GetSalesAnalyticsAsync();
    }
}
