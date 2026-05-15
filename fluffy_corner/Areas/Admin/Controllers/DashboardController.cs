using fluffy_corner.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fluffy_corner.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")] // Ensure only users with the Admin role can access this controller
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;
        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }
        public async Task<IActionResult> Index()
        {
            var stats = await _dashboardService.GetAdminDashboardStatsAsync();

            // تمرير الإحصائيات للـ View باستخدام ViewBag
            ViewBag.TotalUsers = stats["TotalUsers"];
            ViewBag.TotalOrders = stats["TotalOrders"];
            ViewBag.TotalProducts = stats["TotalProducts"];
            ViewBag.TotalCategories = stats["TotalCategories"];

            return View();
            
        }

        // (Orders Management)
        public IActionResult Orders()
        {
          
            return View();
        }

        //(Moderation)
        public IActionResult Moderation()
        {
          
            return View();
        }
    }
}
