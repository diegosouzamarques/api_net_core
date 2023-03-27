using ApiDotNet6.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDotNet6.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetByIdAsync(int id);
        Task<ICollection<Product>> GetProductsAsync();
        Task<Product> CreateAsync(Product product);
        Task<Product> EditAsync(Product product);
        Task DeleteAsync(Product product);
        Task<int> GetIdByCodErpAsync(string codErp);
    }
}
