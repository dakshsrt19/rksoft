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
    }
}