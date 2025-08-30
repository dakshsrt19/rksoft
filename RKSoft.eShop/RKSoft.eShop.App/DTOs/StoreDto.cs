namespace RKSoft.eShop.App.DTOs
{
    public class StoreDTO
    {
        public int Id { get; set; }
        public string StoreName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}