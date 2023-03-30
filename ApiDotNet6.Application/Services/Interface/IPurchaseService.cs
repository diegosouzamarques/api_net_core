using ApiDotNet6.Application.DTOs;

namespace ApiDotNet6.Application.Services.Interface
{
    public interface IPurchaseService
    {
        Task<ResultService<PurchaseDTO>> CreateAsync(PurchaseDTO purchaseDTO);
        Task<ResultService<PurchaseDetailDTO>> GetByIdAsync(int id);
        Task<ResultService<ICollection<PurchaseDetailDTO>>> GetAsync();
        Task<ResultService> DeleteAsync(int id);
        Task<ResultService<PurchaseDTO>> UpdateAsync(PurchaseDTO purchaseDTO);

    }
}
