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
                var result = ResultService.Fail(ex.Message);

                return BadRequest(result);
            }

        }

        [HttpGet]
        [Authorize(Roles = UserRoles.PurchaseRead)]
        public async Task<ActionResult> GetAsync()
        {
            var result = await _purchaseService.GetAsync();
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);

        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = UserRoles.PurchaseRead)]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            var result = await _purchaseService.GetByIdAsync(id);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);

        }

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
                var result = ResultService.Fail(ex.Message);

                return BadRequest(result);
            }

        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = UserRoles.PurchaseDelete)]
        public async Task<ActionResult> RemoveAsync(int id)
        {
            var result = await _purchaseService.DeleteAsync(id);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);

        }

    }
}
