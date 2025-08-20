using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RKSoft.eShop.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty; // For demo only — store hashed passwords in production
        public string PasswordHash { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int UserTypeId { get; set; } // 0: Admin, 1: Customer, 2: Supplier
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public virtual UserType? UserType { get; set; }

        [JsonIgnore]
        public virtual ICollection<UserRoleMapping> UserRoleMappings { get; set; } = new List<UserRoleMapping>();
    }
}
