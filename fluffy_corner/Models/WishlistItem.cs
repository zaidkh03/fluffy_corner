using System.ComponentModel.DataAnnotations;

namespace fluffy_corner.Models
{
    public class WishlistItem
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public string UserId { get; set; } = string.Empty;

        public int ProductId { get; set; }

        public ApplicationUser? User { get; set; }

        public Product? Product { get; set; }
    }
}