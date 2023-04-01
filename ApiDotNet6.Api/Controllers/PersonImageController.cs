using ApiDotNet6.Application.DTOs;
using ApiDotNet6.Application.Services;
using ApiDotNet6.Application.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiDotNet6.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonImageController : ControllerBase
    {
        private readonly IPersonImageService _personImageService;

        public PersonImageController(IPersonImageService personImageService)  
        {
            _personImageService = personImageService;
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Img64Create)]
        public async Task<ActionResult> CreateImageBase64Async(PersonImage64DTO personImageDTO)
        {
            var result = await _personImageService.CreateImageBase64Async(personImageDTO);
           if(result.IsSuccess)
                return Ok(result);

           return BadRequest(result);
        }

        [HttpPost]
        [Route("pathimage")]
        [Authorize(Roles = UserRoles.ImgBytesCreate)]
        public async Task<ActionResult> CreateImageAsync([FromForm] PersonImageFileDTO personImageDTO)
        {
            var result = await _personImageService.CreateImageAsync(personImageDTO);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet]
        [Route("{idperson}")]
        [Authorize(Roles = UserRoles.ImgPersonRead)]
        public async Task<ActionResult> GetByIdAsync(int idperson)
        {
            var result = await _personImageService.GetPersonImageAsync(idperson);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }


        [HttpGet]
        [Route("download/{idimg}")]
        [Authorize(Roles = UserRoles.ImgDownload)]
        public async Task<ActionResult> DownloadImg(int idimg) {

            try
            {
                return await _personImageService.GetDownloadImageAsync(idimg);
            }
            catch (Exception ex)
            {
                var result = ResultService.Fail(ex.Message);

                return BadRequest(result);
            }
       
        
        }

    }
}
