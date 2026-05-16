using Microsoft.AspNetCore.Mvc;

namespace fluffy_corner.Areas.Admin.Controllers
{
    public class ModerationController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
