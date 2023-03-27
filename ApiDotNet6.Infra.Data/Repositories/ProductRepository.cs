using ApiDotNet6.Domain.Entities;
using ApiDotNet6.Domain.Repositories;
using ApiDotNet6.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDotNet6.Infra.Data.Repositories
{
    public class ProductRepository: IProductRepository
    {
        private readonly AppDbContext _db;
        public ProductRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            _db.Add(product);
            await _db.SaveChangesAsync();
            return product;
        }

        public async Task DeleteAsync(Product product)
        {
            _db.Remove(product);
            await _db.SaveChangesAsync();

        }

        public async Task<Product> EditAsync(Product product)
        {
            _db.Update(product);
            await _db.SaveChangesAsync();
            return product;

        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _db.Products.FirstOrDefaultAsync(f => f.Id == id);

        }

        public async Task<int> GetIdByCodErpAsync(string codErp)
        {
            return (await _db.Products.FirstOrDefaultAsync(f => f.CodErp == codErp))?.Id ?? 0;
        }

        public async Task<ICollection<Product>> GetProductsAsync()
        {
            return await _db.Products.ToListAsync();
        }
    }
}
