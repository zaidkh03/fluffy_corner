using fluffy_corner.Data;
using fluffy_corner.ServiceLayer.DTOs;
using fluffy_corner.ServiceLayer.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace fluffy_corner.ServiceLayer.Services
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<OrderSummaryDto>> GetAllOrdersAsync()
        {
            // This is a placeholder implementation. You would typically fetch this data from your database.
            return await _context.Orders
          .Include(o => o.User) // ربط جدول المستخدمين
          .Include(o => o.OrderItems) // ربط تفاصيل الطلب لعدّ المنتجات
          .Select(o => new OrderSummaryDto
          {
              OrderId = o.Id,
              CustomerName = $"{o.User.FirstName} {o.User.LastName}", // أو UserName حسب موديل اليوزر عندك
              OrderDate = o.OrderDate,
              TotalAmount = o.TotalAmount,
              Status = o.Status,
              ItemsCount = o.OrderItems.Count()
          })
          .OrderByDescending(o => o.OrderDate) // الأحدث أولاً
          .ToListAsync();

        }

      
        public async Task<ServiceResult> UpdateOrderStatusAsync(int orderId, string newStatus)
        {
            var order =await  _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
            if (order == null)
                return  new ServiceResult { Success = false, Message = "Order not found." };
            order.Status = newStatus;
            await _context.SaveChangesAsync();
            return new ServiceResult { Success = true , Message="The order status updated successfully "};
        }
    }
}