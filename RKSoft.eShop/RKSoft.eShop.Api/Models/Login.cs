namespace RKSoft.eShop.Api.Models
{
    public class Login
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public string? Email { get; set; }
    }
}
