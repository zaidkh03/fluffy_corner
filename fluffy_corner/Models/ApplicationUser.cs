using Microsoft.AspNetCore.Identity;

namespace fluffy_corner.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? ProfileImageUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation
        public ICollection<Order> Orders { get; set; }
        public ICollection<WishlistItem> WishlistItems { get; set; }
        public ICollection<ProductReview> Reviews { get; set; }
        public ICollection<Testimonial> Testimonials { get; set; }
    }
}
dotnet build