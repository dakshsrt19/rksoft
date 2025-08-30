using RKSoft.eShop.App.Interfaces;
using RKSoft.eShop.Domain.Entities;
using System.Linq.Expressions;

namespace RKSoft.eShop.App.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository CategoryRepository)
        {
            _categoryRepository = CategoryRepository;
        }
        public async Task<Category> CreateCategoryAsync(Category dbRecord)
        {
            return await _categoryRepository.CreateAsync(dbRecord);
        }

        public async Task<bool> DeleteCategoryAsync(Category dbRecord)
        {
            return await _categoryRepository.DeleteAsync(dbRecord);
        }

        public async Task<List<Category>> GetAllCategoryAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }
        public async Task<List<Category>> GetAllActiveCategoryAsync(Expression<Func<Category, bool>> filter)
        {
            return await _categoryRepository.GetAllActiveAsync(filter);
        }

        public async Task<Category> GetCategoryByIdAsync(Expression<Func<Category, bool>> filter, bool useNoTracking = false)
        {
            return await _categoryRepository.GetByIdAsync(filter, useNoTracking);
        }

        public async Task<Category> GetCategoryByNameAsync(Expression<Func<Category, bool>> filter)
        {
            return await _categoryRepository.GetByNameAsync(filter);
        }
        public async Task<Category> UpdateCategoryAsync(Category dbRecord)
        {
            return await _categoryRepository.UpdateAsync(dbRecord);
        }
    }
}
