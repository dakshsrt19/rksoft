using System.Text.Json.Serialization;

namespace RKSoft.eShop.Domain.Entities
{
    public class RolePrivilege
    {
        public int Id { get; set; }
        public string RolePrivilegeName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int RoleId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [JsonIgnore]
        public virtual Role Role { get; set; } = null!;
    }
}
