using ApiDotNet6.Domain.Entities;

namespace ApiDotNet6.Domain.Repositories
{
    public interface IPurchaseRepository
    {
        Task<Purchase> GetByIdAsync(int id);
        Task<ICollection<Purchase>> GetPurchasesAsync();
        Task<Purchase> CreateAsync(Purchase purchase);
        Task<Purchase> EditAsync(Purchase purchase);
        Task DeleteAsync(Purchase purchase);
        Task<ICollection<Purchase>> GetByPersonIdAsync(int personId);
        Task<ICollection<Purchase>> GetByProductIdAsync(int productId);
    }
}
