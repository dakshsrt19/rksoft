using RKSoft.eShop.Domain.Entities;
using System.Linq.Expressions;

namespace RKSoft.eShop.App.Interfaces
{
    public interface IUserService
    {
        Task<User> CreateUserAsync(User dbRecord);
        Task<bool> DeleteUserAsync(User dbRecord);
        Task<List<User>> GetAllUserAsync();
        Task<User> GetUserByIdAsync(Expression<Func<User, bool>> filter, bool useNoTracking = false);
        Task<User> GetUserByNameAsync(Expression<Func<User, bool>> filter);
        Task<User> UpdateUserAsync(User dbRecord);
    }
}
