namespace fluffy_corner.ServiceLayer.DTOs
{
    public class SalesDataDto
    {
        public string Date { get; set; } 
        public int TotalOrders { get; set; }
        public int TotalAmount { get; internal set; }
    }
}
