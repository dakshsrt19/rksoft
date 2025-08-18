using RKSoft.eShop.Domain.Entities;
using System.Linq.Expressions;

namespace RKSoft.eShop.App.Interfaces
{
    public interface IRoleService
    {
        Task<Role> CreateRoleAsync(Role dbRecord);
        Task<bool> DeletRoleAsync(Role dbRecord);
        Task<List<Role>> GetAllRoleAsync();
        Task<Role> GetRoleByIdAsync(Expression<Func<Role, bool>> filter, bool useNoTracking = false);
        Task<Role> GetRoleByNameAsync(Expression<Func<Role, bool>> filter);
        Task<Role> UpdatRoleAsync(Role dbRecord);
    }
}
