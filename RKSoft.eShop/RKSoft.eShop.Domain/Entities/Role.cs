using System.Text.Json.Serialization;

namespace RKSoft.eShop.Domain.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; } = string.Empty; // e.g., Admin, Customer, Supplier
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<RolePrivilege> RolePrivileges { get; set; } = new List<RolePrivilege>();

        [JsonIgnore]
        public virtual ICollection<UserRoleMapping> UserRoleMappings { get; set; } = new List<UserRoleMapping>();
    }
}
