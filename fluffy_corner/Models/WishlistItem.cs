namespace fluffy_corner.Models
{
    public class WishlistItem
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Foreign Keys
        public string UserId { get; set; }
        public int ProductId { get; set; }

        // Navigation
        public ApplicationUser User { get; set; }
        public Product Product { get; set; }
    }
}