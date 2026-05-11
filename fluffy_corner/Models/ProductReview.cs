namespace fluffy_corner.Models
{
    public class ProductReview
    {
        
        public int Id { get; set; }
        
        public int Rating { get; set; }
        
        public string? Comment { get; set; }
        
        public bool IsApproved { get; set; } = false;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Foreign Keys
        public int ProductId { get; set; }
        public string UserId { get; set; }

        // Navigation
        public Product Product { get; set; }
        public ApplicationUser User { get; set; }
    }
}