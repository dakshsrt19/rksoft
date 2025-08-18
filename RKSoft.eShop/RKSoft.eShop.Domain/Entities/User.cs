using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RKSoft.eShop.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public int UserTypeId { get; set; } // 0: Admin, 1: Customer, 2: Supplier
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public virtual UserType UserType { get; set; } // Navigation property for user type

        public virtual ICollection<UserRoleMapping> UserRoleMappings { get; set; } // Navigation property for roles associated with the user
    }
}
