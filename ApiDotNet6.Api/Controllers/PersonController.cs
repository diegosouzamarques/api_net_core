using ApiDotNet6.Application.DTOs;
using ApiDotNet6.Application.Services.Interface;
using ApiDotNet6.Domain.FiltersDb;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiDotNet6.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        private List<string> _permissionNeeded = new List<string>() { "admin" };
        private readonly List<string> _permissionUser;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        #region Documentation
        // POST api/Person
        /// <summary>
        /// Cria uma pessoa na entidade Person
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST
        ///     {
        ///       "name": "Diego Marques",
        ///       "document": "546.408.010-41",
        ///       "phone": "+55 84 97706-7368"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">
        /// Retorno será uma pessoa criada com seu código id 
        /// </response>
        /// <response code="400">Retorno com descrição do esta faltando na requisição
        /// </response>  
        #endregion
        [HttpPost]
        [Authorize(Roles = UserRoles.PersonCreate)]
        public async Task<ActionResult> PostAsync([FromBody] PersonDTO personDTO)
        {
            try {
                var result = await _personService.CreateAsync(personDTO);
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
        // Get api/Person
        /// <summary>
        /// Busca todas as Pessoas Cadastradas
        /// </summary>
        /// <response code="200">
        /// Retorno lista contendo todas as pessoas
        /// </response>
        /// <response code="400">Retorno com descrição do esta faltando na requisição
        /// </response>  
        #endregion
        [HttpGet]
        [Authorize(Roles = UserRoles.PersonRead)]
        public async Task<ActionResult> GetAsync()
        {
            try
            {
                var result = await _personService.GetAsync();
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
        // Get api/Person/{id}
        /// <summary>
        /// Busca uma pessoa pelo código do ID
        /// </summary>
        /// <response code="200">
        ///    Retorno da pessoa localizada pelo código ID
        /// </response>
        /// <response code="400">Retorno com descrição do esta faltando na requisição
        /// </response>  
        #endregion
        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = UserRoles.PersonRead)]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            try
            {
                var result = await _personService.GetByIdAsync(id);
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
        // PUT api/Person
        /// <summary>
        /// Atualiza informações de uma pessoa na entidade Person
        /// </summary>
        /// <remarks>
        /// Exemplo:
        ///
        ///     POST
        ///     {
        ///       "id": 5,
        ///       "name": "Diego Marques",
        ///       "document": "546.408.010-41",
        ///       "phone": "+55 84 97706-7368"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">
        /// Retorno será a pessoa atualizad com as informações enviadas
        /// </response>
        /// <response code="400">Retorno com descrição do esta faltando na requisição
        /// </response>  
        #endregion
        [HttpPut]
        [Authorize(Roles = UserRoles.PersonUpdate)]
        public async Task<ActionResult> UpdateAsync([FromBody] PersonDTO personDTO)
        {
            try
            {
                var result = await _personService.UpdateAsync(personDTO);
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
        // Delete api/Person/{id}
        /// <summary>
        ///  Remove uma pessoa pelo código do ID
        /// </summary>
        /// <response code="200">
        ///    Retorno será que pessoa foi removida com sucesso
        /// </response>
        /// <response code="400">Retorno com descrição do esta faltando na requisição
        /// </response>  
        #endregion
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = UserRoles.PersonDelete)]
        public async Task<ActionResult> DeleteAsync(int id)
        {

            try
            {
                var result = await _personService.DeleteAsync(id);
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
        // Get api/Person
        /// <summary>
        /// Busca todas as Pessoas Cadastradas de forma páginada
        /// </summary>
        /// <response code="200">
        /// Retorno lista contendo as pessoas da páginada solicitada
        /// </response>
        /// <response code="400">Retorno com descrição do esta faltando na requisição
        /// </response>  
        #endregion
        [HttpGet]
        [Route("paged")]
        [Authorize(Roles = UserRoles.PersonRead)]
        public async Task<ActionResult> GetPagedAsync([FromQuery] PersonFilterDb personFilterDb)
        {
            try
            {
                var result = await _personService.GetPagedAsync(personFilterDb);
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
