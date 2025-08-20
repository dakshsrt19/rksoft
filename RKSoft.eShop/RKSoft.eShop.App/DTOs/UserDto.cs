using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RKSoft.eShop.App.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; } = string.Empty;

        // Optional - remove this from DTO if you're not using it in API responses
        public string? PasswordHash { get; set; }

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Range(0, 2, ErrorMessage = "UserTypeId must be 0 (Admin), 1 (Customer), or 2 (Supplier).")]
        public int UserTypeId { get; set; }

        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
