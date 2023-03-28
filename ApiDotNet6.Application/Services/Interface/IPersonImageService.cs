using ApiDotNet6.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDotNet6.Application.Services.Interface
{
    public interface IPersonImageService
    {
        Task<ResultService> CreateImageBase64Async(PersonImageDTO personImageDTO);
        Task<ResultService> CreateImageAsync(PersonImageDTO personImageDTO);
    }
}
