using System.ComponentModel.DataAnnotations;

namespace fluffy_corner.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string SKU { get; set; } = string.Empty;

        [StringLength(300)]
        public string? ShortDescription { get; set; }

        public string? Description { get; set; }

        [Range(0.01, 10000)]
        public decimal Price { get; set; }

        [Range(0, 100)]
        public decimal? DiscountPercentage { get; set; }

        [Range(0, 10000)]
        public int StockQuantity { get; set; }

        public decimal? Weight { get; set; }

        [StringLength(100)]
        public string? Brand { get; set; }

        [Required]
        public string AnimalType { get; set; } = "Both"; // Cat, Dog, Both

        public bool IsFeatured { get; set; } = false;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        public int CategoryId { get; set; }

        public Category? Category { get; set; }

        public ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
        public ICollection<ProductReview> Reviews { get; set; } = new List<ProductReview>();
        public ICollection<WishlistItem> WishlistItems { get; set; } = new List<WishlistItem>();
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}