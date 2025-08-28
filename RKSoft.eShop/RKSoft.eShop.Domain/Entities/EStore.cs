namespace RKSoft.eShop.Domain.Entities
{
    public class EStore
    {
        public int Id { get; set; }
        public string StoreName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<Category> Categories { get; set; }
    }
}