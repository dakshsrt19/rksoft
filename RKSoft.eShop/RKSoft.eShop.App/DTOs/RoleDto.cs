using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RKSoft.eShop.App.DTOs
{
    public class RoleDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Role name is required.")]
        public string RoleName { get; set; } = string.Empty;

        public string? Description { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
