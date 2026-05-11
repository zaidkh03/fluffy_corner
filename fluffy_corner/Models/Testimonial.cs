namespace fluffy_corner.Models
{
    public class Testimonial
    {
        public int Id { get; set; }

        public string Message { get; set; }
        
        public bool IsApproved { get; set; } = false;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Foreign Key
        public string UserId { get; set; }

        // Navigation
        public ApplicationUser User { get; set; }
    }
}