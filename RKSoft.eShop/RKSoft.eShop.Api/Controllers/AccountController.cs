using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RKSoft.eShop.Api.Models;
using RKSoft.eShop.App.Services;
using RKSoft.eShop.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RKSoft.eShop.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly RoleService _roleService;
        private readonly IConfiguration _configuration;

        public AccountController(UserService userService, RoleService roleService, IConfiguration configuration)
        {
            _userService = userService;
            _roleService = roleService;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login model)
        {
            if (model == null ||
                (string.IsNullOrEmpty(model.Email) && string.IsNullOrEmpty(model.UserName)) ||
                string.IsNullOrEmpty(model.Password))
            {
                return BadRequest("Invalid login details.");
            }

            string userName = !string.IsNullOrEmpty(model.UserName) ? model.UserName : model.Email!;

            var user = await _userService.GetUserByNameAsync(u => u.UserName == userName);

            if (user == null || user.Password != model.Password)
            {
                return Unauthorized("Invalid username or password.");
            }

            var userRoles = await _roleService.GetRolesByUserAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName ?? ""),
                new Claim("username", user.UserName ?? ""),
                new Claim("email", user.Email ?? ""),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var roleNames = userRoles.Select(r => r.Role.RoleName).ToList();
            authClaims.AddRange(roleNames.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(key))
            {
                throw new InvalidOperationException("JWT key is missing from configuration.");
            }

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var tokenValidityMinutes = double.TryParse(_configuration["Jwt:TokenValidityMins"], out var mins) ? mins : 60;

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddMinutes(tokenValidityMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo,
                username = user.UserName,
                roles = roleNames
            });
        }
    }
}
