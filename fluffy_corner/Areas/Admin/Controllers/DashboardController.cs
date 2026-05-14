using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fluffy_corner.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")] // Ensure only users with the Admin role can access this controller
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
