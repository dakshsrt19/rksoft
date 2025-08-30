using RKSoft.eShop.Domain.Entities;

namespace RKSoft.eShop.App.DTOs
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int StoreId { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // ✅ Avoid EF entities in DTO
        public string? StoreName { get; set; }
    }
}
