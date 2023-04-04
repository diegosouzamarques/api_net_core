using ApiDotNet6.Application.DTOs;
using ApiDotNet6.Application.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiDotNet6.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        #region Documentation
        // POST api/Product
        /// <summary>
        /// Cria um produto na entidade Product
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST
        ///     {
        ///       "name": "Maionese Da Boa",
        ///       "codErp": "20304050",
        ///       "price":  9.45
        ///     }
        ///
        /// </remarks>
        /// <response code="200">
        /// Retorno será um produto criado com seu código id 
        /// </response>
        /// <response code="400">Retorno com descrição do esta faltando na requisição
        /// </response>  
        #endregion
        [HttpPost]
        [Authorize(Roles = UserRoles.ProductCreate)]
        public async Task<ActionResult> PostAsync([FromBody] ProductDTO productDTO)
        {
            var result = await _productService.CreateAsync(productDTO);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        #region Documentation
        // Get api/Product
        /// <summary>
        /// Busca todos os produtos Cadastradas
        /// </summary>
        /// <response code="200">
        /// Retorno lista contendo todos os produtos
        /// </response>
        /// <response code="400">Retorno com descrição do esta faltando na requisição
        /// </response>  
        #endregion
        [HttpGet]
        [Authorize(Roles = UserRoles.ProductRead)]
        public async Task<ActionResult> GetAsync()
        {
            var result = await _productService.GetAsync();
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        #region Documentation
        // Get api/Product/{id}
        /// <summary>
        /// Busca um produto pelo código do ID
        /// </summary>
        /// <response code="200">
        ///    Retorno do produto localizado pelo código ID
        /// </response>
        /// <response code="400">Retorno com descrição do esta faltando na requisição
        /// </response>  
        #endregion
        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = UserRoles.ProductRead)]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            var result = await _productService.GetByIdAsync(id);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        #region Documentation
        // PUT api/Product
        /// <summary>
        /// Atualiza informações de um produto na entidade Product
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST
        ///     {
        ///       "id": 5,
        ///       "name": "Maionese Da Boa",
        ///       "codErp": "2030405060",
        ///       "price": 10.45
        ///     }
        ///
        /// </remarks>
        /// <response code="200">
        /// Retorno será o produto atualizado com as informações enviadas
        /// </response>
        /// <response code="400">Retorno com descrição do esta faltando na requisição
        /// </response>  
        #endregion
        [HttpPut]
        [Authorize(Roles = UserRoles.ProductUpdate)]
        public async Task<ActionResult> UpdateAsync([FromBody] ProductDTO productDTO)
        {
            var result = await _productService.UpdateAsync(productDTO);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        #region Documentation
        // Delete api/Product/{id}
        /// <summary>
        ///  Remove um produto pelo código do ID
        /// </summary>
        /// <response code="200">
        ///    Retorno será que o produto foi removido com sucesso
        /// </response>
        /// <response code="400">Retorno com descrição do esta faltando na requisição
        /// </response>  
        #endregion
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = UserRoles.ProductDelete)]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var result = await _productService.DeleteAsync(id);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

    }
}
