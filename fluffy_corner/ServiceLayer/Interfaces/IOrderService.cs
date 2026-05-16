using fluffy_corner.ServiceLayer.DTOs;

namespace fluffy_corner.ServiceLayer.Interfaces
{
    public interface IOrderService
    {
        Task<List<OrderSummaryDto>> GetAllOrdersAsync();
        Task<ServiceResult> UpdateOrderStatusAsync(int orderId, string newStatus);
    }
}
