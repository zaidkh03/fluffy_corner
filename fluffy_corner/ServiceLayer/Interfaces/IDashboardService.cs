namespace fluffy_corner.ServiceLayer.Interfaces
{
    public interface IDashboardService
    {
        Task<Dictionary<string, int>> GetAdminDashboardStatsAsync();
    }
}
