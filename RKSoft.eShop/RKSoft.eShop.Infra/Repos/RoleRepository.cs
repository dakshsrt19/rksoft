using Microsoft.EntityFrameworkCore;
using RKSoft.eShop.App.Interfaces;
using RKSoft.eShop.Domain.Entities;
using RKSoft.eShop.Infra.Data;
using RKSoft.eShop.Infra.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RKSoft.eShop.Infra.Repos
{
    public class RoleRepository : AppRepository<Role>, IRoleRepository
    {
        private readonly AppDbContext _appDbContext;
        public RoleRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<UserRoleMapping>> GetUserRolesAsync(User dbRecord)
        {
            if (dbRecord == null)
                throw new ArgumentNullException(nameof(dbRecord));
            return await _appDbContext.UserRoleMappings
                          .Where(mapping => mapping.UserId == dbRecord.Id)
                          .Include(mapping => mapping.Role)  // Include Role entity
                          .Include(mapping => mapping.User)  // Optional: Include User
                          .ToListAsync();
        }

    }
}