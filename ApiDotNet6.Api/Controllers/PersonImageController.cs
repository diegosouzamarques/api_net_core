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

        #region Documentation
        // POST api/PersonImage
        /// <summary>
        /// Cria registro e armazenagem da imagem vinculada a uma pessoa
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST
        ///     {
        ///       "personId": "5",
        ///       "description": "Foto de perfil",
        ///       "image": "/9j/4AAQSkZJRgABAQAAAQAB... imagem em base64"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">
        /// Retorno será imagem registrada e armazenagem da imagem 
        /// </response>
        /// <response code="400">Retorno com descrição do esta faltando na requisição
        /// </response>  
        #endregion
        [HttpPost]
        [Authorize(Roles = UserRoles.Img64Create)]
        public async Task<ActionResult> CreateImageBase64Async(PersonImage64DTO personImageDTO)
        {
            try
            {
                var result = await _personImageService.CreateImageBase64Async(personImageDTO);
                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                var result = StatusCode(StatusCodes.Status500InternalServerError, ex.GetaAllMessages());
                return result;
            }
        }

        #region Documentation
        // POST api/PersonImage/pathimage
        /// <summary>
        /// Cria registro e armazenagem da imagem vinculada a uma pessoa
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST
        ///     {
        ///       "personId": "5",
        ///       "description": "Foto de Perfil",
        ///       "image": "imagem em bytes"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">
        /// Retorno será imagem registrada e armazenagem da imagem 
        /// </response>
        /// <response code="400">Retorno com descrição do esta faltando na requisição
        /// </response>  
        #endregion
        [HttpPost]
        [Route("pathimage")]
        [Authorize(Roles = UserRoles.ImgBytesCreate)]
        public async Task<ActionResult> CreateImageAsync([FromForm] PersonImageFileDTO personImageDTO)
        {

            try
            {
                var result = await _personImageService.CreateImageAsync(personImageDTO);
                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                var result = StatusCode(StatusCodes.Status500InternalServerError, ex.GetaAllMessages());
                return result;
            }

        }

        #region Documentation
        // Get api/PersonImage/{idperson}
        /// <summary>
        /// Busca todos registros de imagem de uma pessoa pelo código do ID
        /// </summary>
        /// <response code="200">
        ///    Retorno será uma lista contendo todas as descrições das imagens da pessoa localizada pelo código ID
        /// </response>
        /// <response code="400">Retorno com descrição do esta faltando na requisição
        /// </response>  
        #endregion
        [HttpGet]
        [Route("{idperson}")]
        [Authorize(Roles = UserRoles.ImgPersonRead)]
        public async Task<ActionResult> GetByIdAsync(int idperson)
        {
            try
            {
                var result = await _personImageService.GetPersonImageAsync(idperson);
                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                var result = StatusCode(StatusCodes.Status500InternalServerError, ex.GetaAllMessages());
                return result;
            }
        }


        #region Documentation
        // Get api/PersonImage/download/{idimg}
        /// <summary>
        /// Busca imagem localizada através do código id informado
        /// </summary>
        /// <response code="200">
        ///    Retorno será download da imagem localizada
        /// </response>
        /// <response code="400">Retorno com descrição do esta faltando na requisição
        /// </response>  
        #endregion
        [HttpGet]
        [Route("download/{idimg}")]
        [Authorize(Roles = UserRoles.ImgDownload)]
        public async Task<ActionResult> DownloadImg(int idimg)
        {

            try
            {
                return await _personImageService.GetDownloadImageAsync(idimg);
            }
            catch (Exception ex)
            {
                var result = ResultService.Fail(ex.GetaAllMessages());

                return BadRequest(result);
            }


        }

    }
}
