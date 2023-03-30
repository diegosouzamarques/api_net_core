using ApiDotNet6.Application.DTOs;

namespace ApiDotNet6.Application.Services.Interface
{
    public interface IProductService
    {
        Task<ResultService<ProductDTO>> CreateAsync(ProductDTO productDTO);
        Task<ResultService<ICollection<ProductDTO>>> GetAsync();
        Task<ResultService<ProductDTO>> GetByIdAsync(int id);
        Task<ResultService<ProductDTO>> UpdateAsync(ProductDTO productDTO);
        Task<ResultService> DeleteAsync(int id);
    }
}
