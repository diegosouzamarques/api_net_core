using ApiDotNet6.Application.DTOs;

namespace ApiDotNet6.Application.Services.Interface
{
    public interface IPersonImageService
    {
        Task<ResultService> CreateImageBase64Async(PersonImageDTO personImageDTO);
        Task<ResultService> CreateImageAsync(PersonImageDTO personImageDTO);
    }
}
