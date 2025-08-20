using RKSoft.eShop.App.Interfaces;
using RKSoft.eShop.Domain.Entities;
using System.Linq.Expressions;

namespace RKSoft.eShop.App.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _RoleRepository;
        public RoleService(IRoleRepository RoleRepository)
        {
            _RoleRepository = RoleRepository;
        }
        public async Task<Role> CreateRoleAsync(Role dbRecord)
        {
            return await _RoleRepository.CreateAsync(dbRecord);
        }

        public async Task<bool> DeletRoleAsync(Role dbRecord)
        {
            return await _RoleRepository.DeleteAsync(dbRecord);
        }

        public async Task<List<Role>> GetAllRoleAsync()
        {
            return await _RoleRepository.GetAllAsync();
        }


        public async Task<Role> GetRoleByIdAsync(Expression<Func<Role, bool>> filter, bool useNoTracking = false)
        {
            return await _RoleRepository.GetByIdAsync(filter, useNoTracking);
        }

        public async Task<Role> GetRoleByNameAsync(Expression<Func<Role, bool>> filter)
        {
            return await _RoleRepository.GetByNameAsync(filter);
        }
        public async Task<Role> UpdatRoleAsync(Role dbRecord)
        {
            return await _RoleRepository.UpdateAsync(dbRecord);
        }

        public async Task<List<UserRoleMapping>> GetRolesByUserAsync(User dbRecord)
        {
            return await _RoleRepository.GetUserRolesAsync(dbRecord);
        }
    }
}
