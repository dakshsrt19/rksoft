using RKSoft.eShop.Domain.Entities;

namespace RKSoft.eShop.App.Interfaces
{
    public interface ICategoryRepository : IAppRepository<Category>
    {
        //Task<List<Category>> GetAllCategoriesAsync_Status(Category dbRecord);
    }
}
