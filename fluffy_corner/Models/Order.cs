namespace fluffy_corner.Models
{
    public class Order
    {
       
        public int Id { get; set; }
        
        public string OrderNumber { get; set; }
        
        public DateTime OrderDate { get; set; } = DateTime.Now;
        
        public string Status { get; set; } = "Pending";
        
        public decimal SubTotal { get; set; }
        
        public decimal ShippingFee { get; set; }
        
        public decimal TotalAmount { get; set; }
        
        public string PaymentMethod { get; set; }
        
        public string PaymentStatus { get; set; }
        
        
        public string ShippingAddress { get; set; }
        
        public string City { get; set; }
        
        public string Country { get; set; }
        
        public string? Notes { get; set; }

        // Foreign Key
        public string UserId { get; set; }

        // Navigation
        public ApplicationUser User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}