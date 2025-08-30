using RKSoft.eShop.Domain.Entities;
using System.Linq.Expressions;

namespace RKSoft.eShop.App.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> CreateCategoryAsync(Category dbRecord);
        Task<bool> DeleteCategoryAsync(Category dbRecord);
        Task<List<Category>> GetAllCategoryAsync();
        Task<List<Category>> GetAllActiveCategoryAsync(Expression<Func<Category, bool>> filter);
        Task<Category> GetCategoryByIdAsync(Expression<Func<Category, bool>> filter, bool useNoTracking = false);
        Task<Category> GetCategoryByNameAsync(Expression<Func<Category, bool>> filter);
        Task<Category> UpdateCategoryAsync(Category dbRecord);
    }
}
