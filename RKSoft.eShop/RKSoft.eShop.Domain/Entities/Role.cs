using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RKSoft.eShop.Domain.Entities
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; } // e.g., Admin, Customer, Supplier
        public string Description { get; set; } // Optional description of the role
        public bool IsActive { get; set; } // Indicates if the role is active
        public bool IsDeleted { get; set; } // Indicates if the role is deleted
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Timestamp for when the role was created
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow; // Timestamp for when the role was last updated
    
        public virtual ICollection<RolePrivilege> RolePrivileges { get; set; }
    
        public virtual ICollection<UserRoleMapping> UserRoleMappings { get; set; } // Navigation property for users associated with the role
    }
}
