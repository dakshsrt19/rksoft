using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RKSoft.eShop.Domain.Entities
{
    public class RolePrivilege
    {
        public int Id { get; set; } // Unique identifier for the role privilege
        public string RolePrivilegeName { get; set; } // Foreign key to the Privilege entity
        public string Description { get; set; }
        public int RoleId { get; set; } // Foreign key to the Role entity
        public bool IsActive { get; set; } // Indicates if the role privilege is active
        public bool IsDeleted { get; set; } // Indicates if the role privilege is deleted
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Timestamp for when the role privilege was created
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow; // Timestamp for when the role privilege was last updated
        
        public virtual Role Role { get; set; } // Navigation property to the Role entity
    }
}
