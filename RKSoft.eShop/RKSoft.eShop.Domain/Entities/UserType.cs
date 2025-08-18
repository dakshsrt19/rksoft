using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RKSoft.eShop.Domain.Entities
{
    public class UserType
    {
        public int Id { get; set; } // Unique identifier for the user type
        public string Name { get; set; } // Name of the user type (e.g., Admin, Customer, Supplier) 
        public string Description { get; set; } // Optional description of the user type
    
        public virtual ICollection<User> Users { get; set; } // Navigation property for users associated with this user type
    }
}
