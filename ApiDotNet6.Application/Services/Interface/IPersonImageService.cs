using ApiDotNet6.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ApiDotNet6.Application.Services.Interface
{
    public interface IPersonImageService
    {
        Task<ResultService> CreateImageBase64Async(PersonImage64DTO personImageDTO);
        Task<ResultService> CreateImageAsync(PersonImageFileDTO personImageDTO);
        Task<ResultService> GetPersonImageAsync(int idPerson);
        Task<ActionResult> GetDownloadImageAsync(int idimg);
    }
}
