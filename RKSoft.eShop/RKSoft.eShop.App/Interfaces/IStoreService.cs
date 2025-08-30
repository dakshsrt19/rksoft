using Microsoft.AspNetCore.JsonPatch;
using RKSoft.eShop.Domain.Entities;
using System.Linq.Expressions;

namespace RKSoft.eShop.App.Interfaces
{
    public interface IStoreService 
    {
        Task<EStore> CreateStoreAsync(EStore dbRecord);
        Task<bool> DeleteStoreAsync(EStore dbRecord);
        Task<List<EStore>> GetAllStoreAsync();
        Task<List<EStore>> GetAllActiveStoreAsync(Expression<Func<EStore, bool>> filter);
        Task<EStore> GetStoreByIdAsync(Expression<Func<EStore, bool>> filter, bool useNoTracking = false);
        Task<EStore> GetStoreByNameAsync(Expression<Func<EStore, bool>> filter);
        Task<EStore> UpdateStoreAsync(EStore dbRecord);
        Task<EStore?> PartialUpdateStoreAsync(EStore updatedStore);
    }
}