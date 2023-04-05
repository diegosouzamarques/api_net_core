using ApiDotNet6.Application.DTOs;
using ApiDotNet6.Application.Services;
using ApiDotNet6.Application.Services.Interface;
using ApiDotNet6.Domain.Validations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiDotNet6.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;

        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        #region Documentation
        // POST api/Purchase
        /// <summary>
        /// Cria uma compra na entidade Purchase
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST
        ///     {
        ///       "codErp": "20304050",
        ///       "document": "546.408.010-41"   
        ///     }
        ///
        /// </remarks>
        /// <response code="200">
        /// Retorno será uma compra criada com seu código id 
        /// </response>
        /// <response code="400">Retorno com descrição do esta faltando na requisição
        /// </response>  
        #endregion
        [HttpPost]
        [Authorize(Roles = UserRoles.PurchaseCreate)]
        public async Task<ActionResult> PostAsync([FromBody] PurchaseDTO purchaseDTO)
        {
            try
            {
                var result = await _purchaseService.CreateAsync(purchaseDTO);
                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }
            catch (DomainValidationException ex)
            {
                var result = ResultService.Fail(ex.GetaAllMessages());

                return BadRequest(result);
            }

        }

        #region Documentation
        // Get api/Purchase
        /// <summary>
        /// Busca todas as compras Cadastradas
        /// </summary>
        /// <response code="200">
        /// Retorno lista contendo todas as compras
        /// </response>
        /// <response code="400">Retorno com descrição do esta faltando na requisição
        /// </response>  
        #endregion
        [HttpGet]
        [Authorize(Roles = UserRoles.PurchaseRead)]
        public async Task<ActionResult> GetAsync()
        {
            try
            {
                var result = await _purchaseService.GetAsync();
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
        // Get api/Purchase/{id}
        /// <summary>
        /// Busca uma compra pelo código do ID
        /// </summary>
        /// <response code="200">
        ///    Retorno da compra localizada pelo código ID
        /// </response>
        /// <response code="400">Retorno com descrição do esta faltando na requisição
        /// </response>  
        #endregion
        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = UserRoles.PurchaseRead)]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            try
            {
                var result = await _purchaseService.GetByIdAsync(id);
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
        // PUT api/Purchase
        /// <summary>
        /// Atualiza informações de uma compra na entidade Purchase
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST
        ///     {
        ///       "id": 5,
        ///       "codErp": "20304050",
        ///       "document": "546.408.010-41" 
        ///     }
        ///
        /// </remarks>
        /// <response code="200">
        /// Retorno será a compra atualizada com as informações enviadas
        /// </response>
        /// <response code="400">Retorno com descrição do esta faltando na requisição
        /// </response>  
        #endregion
        [HttpPut]
        [Authorize(Roles = UserRoles.PurchaseUpdate)]
        public async Task<ActionResult> EditAsync([FromBody] PurchaseDTO purchaseDTO)
        {
            try
            {
                var result = await _purchaseService.UpdateAsync(purchaseDTO);
                if (result.IsSuccess)
                    return Ok(result);

                return BadRequest(result);
            }
            catch (DomainValidationException ex)
            {
                var result = ResultService.Fail(ex.GetaAllMessages());

                return BadRequest(result);
            }

        }

        #region Documentation
        // Delete api/Purchase/{id}
        /// <summary>
        ///  Remove uma compra pelo código do ID
        /// </summary>
        /// <response code="200">
        ///    Retorno será que compra foi removida com sucesso
        /// </response>
        /// <response code="400">Retorno com descrição do esta faltando na requisição
        /// </response>  
        #endregion
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = UserRoles.PurchaseDelete)]
        public async Task<ActionResult> RemoveAsync(int id)
        {
            try
            {
                var result = await _purchaseService.DeleteAsync(id);
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

    }
}
