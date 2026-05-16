namespace fluffy_corner.ServiceLayer.DTOs
{
    public class OrderSummaryDto
    {
        public int OrderId { get; set; }
        public string? CustomerName { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string? Status { get; set; } 
        public int ItemsCount { get; set; }
    }
}
