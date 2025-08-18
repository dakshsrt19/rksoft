using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RKSoft.eShop.Domain.Entities
{
    public class UserRoleMapping
    {
        public int Id { get; set; } // Unique identifier for the user role mapping
        public int UserId { get; set; } // Foreign key to the User entity
        public int RoleId { get; set; } // Foreign key to the Role entity
    
        public User User { get; set; } // Navigation property to the User entity
        public Role Role { get; set; } // Navigation property to the Role entity    
    }
}
