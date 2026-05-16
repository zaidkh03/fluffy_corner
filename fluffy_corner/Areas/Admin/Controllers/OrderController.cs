using fluffy_corner.ServiceLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace fluffy_corner.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return View(orders);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(int orderId ,String newStatus)
        {
            var result = await _orderService.UpdateOrderStatusAsync(orderId, newStatus);
            if(result.Success)
            {
                return RedirectToAction("Index");
            }
            else
            {
                // Handle error (e.g., show an error message)
                TempData["ErrorMessage"] = result.Message;
                return RedirectToAction("Index");
            }
        }
      
    }
}
