namespace RKSoft.eShop.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int StoreId { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // ✅ Proper navigation property
        public virtual EStore Store { get; set; } = null!;
    }
}
