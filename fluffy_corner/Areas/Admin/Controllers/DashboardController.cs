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

        // (Constructor Injection)
        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        // الصفحة الرئيسية للأدمن (Dashboard)
        public async Task<IActionResult> Index()
        {
            // جلب الإحصائيات من الـ Service Layer
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
            //  OrderService
            return View();
        }

        //  (Moderation)
        public IActionResult Moderation()
        {
            //(Approve/Reject)
            return View();
        }
    }
}
