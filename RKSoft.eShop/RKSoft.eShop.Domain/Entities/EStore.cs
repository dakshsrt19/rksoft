namespace RKSoft.eShop.Domain.Entities
{
    public class EStore
    {
        public int Id { get; set; }
        public string StoreName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // ✅ Initialize collection to avoid nulls
        public virtual ICollection<Category> Categories { get; set; } = new HashSet<Category>();
    }
}