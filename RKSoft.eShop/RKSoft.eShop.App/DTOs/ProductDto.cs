using RKSoft.eShop.Domain.Entities;

namespace RKSoft.eShop.App.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
