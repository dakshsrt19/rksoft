using RKSoft.eShop.App.Interfaces;
using RKSoft.eShop.Domain.Entities;
using RKSoft.eShop.Infra.Data;
using RKSoft.eShop.Infra.Repositories;

namespace RKSoft.eShop.Infra.Repos
{
    public class UserRepository : AppRepository<User>, IUserRepository
    {
        private readonly AppDbContext _appDbContext;
        public UserRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }
    }
}