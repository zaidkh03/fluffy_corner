using Microsoft.AspNetCore.Mvc;

namespace fluffy_corner.Areas.Admin.Controllers
{
    public class AnimalsController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
