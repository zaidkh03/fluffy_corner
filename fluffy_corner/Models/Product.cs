namespace fluffy_corner.Models
{
    public class Product
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string SKU { get; set; }
        
        public string? ShortDescription { get; set; }
        
        public string? Description { get; set; }
        
        public decimal Price { get; set; }
        
        public decimal? DiscountPercentage { get; set; }
        
        public int StockQuantity { get; set; }
        
        public decimal? Weight { get; set; }
        
        public string? Brand { get; set; }
        
        public string AnimalType { get; set; } // "Cat", "Dog", "Both"
        
        public bool IsFeatured { get; set; } = false;
        
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public DateTime? UpdatedAt { get; set; }

        // Foreign Key
        public int CategoryId { get; set; }

        // Navigation
        public Category Category { get; set; }
        public ICollection<ProductImage> Images { get; set; }
        public ICollection<ProductReview> Reviews { get; set; }
        public ICollection<WishlistItem> WishlistItems { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}