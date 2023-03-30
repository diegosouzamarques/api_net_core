using ApiDotNet6.Domain.Entities;
using ApiDotNet6.Domain.Repositories;
using ApiDotNet6.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace ApiDotNet6.Infra.Data.Repositories
{

    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly AppDbContext _db;
        public PurchaseRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Purchase> CreateAsync(Purchase purchase)
        {
            _db.Add(purchase);
            await _db.SaveChangesAsync();
            return purchase;
        }

        public async Task DeleteAsync(Purchase purchase)
        {
            _db.Remove(purchase);
            await _db.SaveChangesAsync();

        }

        public async Task<Purchase> EditAsync(Purchase purchase)
        {
            _db.Update(purchase);
            await _db.SaveChangesAsync();
            return purchase;

        }

        public async Task<Purchase> GetByIdAsync(int id)
        {
            return await _db.Purchases
                .Include(x => x.Person)
                .Include(x => x.Product).FirstOrDefaultAsync(f => f.Id == id);

        }

        public async Task<ICollection<Purchase>> GetPurchasesAsync()
        {
            return await _db.Purchases
                .Include(x => x.Person)
                .Include(x => x.Product).ToListAsync();
        }

        public async Task<ICollection<Purchase>> GetByPersonIdAsync(int personId)
        {
            return await _db.Purchases
                .Include(x => x.Person)
                .Include(x => x.Product)
                .Where(x => x.PersonId == personId).ToListAsync();

        }

        public async Task<ICollection<Purchase>> GetByProductIdAsync(int productId)
        {
            return await _db.Purchases
                .Include(x => x.Person)
                .Include(x => x.Product)
                .Where(x => x.ProductId == productId).ToListAsync();

        }
    }
}
