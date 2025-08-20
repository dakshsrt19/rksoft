using System.Text.Json.Serialization;

namespace RKSoft.eShop.Domain.Entities
{
    public class UserRoleMapping
    {
        public int Id { get; set; }
        public int UserId { get; set; } // FK to User
        public int RoleId { get; set; } // FK to Role

        [JsonIgnore]
        public virtual User User { get; set; } = null!;

        public virtual Role Role { get; set; } = null!;
    }
}
