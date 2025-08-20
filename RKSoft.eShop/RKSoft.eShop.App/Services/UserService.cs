using RKSoft.eShop.App.Interfaces;
using RKSoft.eShop.Domain.Entities;
using System.Linq.Expressions;

namespace RKSoft.eShop.App.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _UserRepository;
        public UserService(IUserRepository UserRepository)
        {
            _UserRepository = UserRepository;
        }
        public async Task<User> CreateUserAsync(User dbRecord)
        {
            return await _UserRepository.CreateAsync(dbRecord);
        }

        public async Task<bool> DeleteUserAsync(User dbRecord)
        {
            return await _UserRepository.DeleteAsync(dbRecord);
        }

        public async Task<List<User>> GetAllUserAsync()
        {
            return await _UserRepository.GetAllAsync();
        }


        public async Task<User> GetUserByIdAsync(Expression<Func<User, bool>> filter, bool useNoTracking = false)
        {
            return await _UserRepository.GetByIdAsync(filter, useNoTracking);
        }

        public async Task<User> GetUserByNameAsync(Expression<Func<User, bool>> filter)
        {
            return await _UserRepository.GetByNameAsync(filter);
        }
        public async Task<User> UpdateUserAsync(User dbRecord)
        {
            return await _UserRepository.UpdateAsync(dbRecord);
        }
    }
}
