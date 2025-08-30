using Microsoft.AspNetCore.JsonPatch;
using System.Linq.Expressions;

namespace RKSoft.eShop.App.Interfaces
{
    public interface IAppRepository<T>
    {
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllActiveAsync(Expression<Func<T, bool>> filter);
        Task<T> GetByIdAsync(Expression<Func<T, bool>> filter, bool useNoTracking = false);
        Task<T> GetByNameAsync(Expression<Func<T, bool>> filter);
        Task<T> CreateAsync(T dbRecord);
        Task<T> UpdateAsync(T dbRecord);
        Task<bool> DeleteAsync(T dbRecord);
    }
}