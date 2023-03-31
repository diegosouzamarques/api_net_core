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

        [HttpPost]
        [Authorize(Roles = UserRoles.PersonCreate)]
        public async Task<IActionResult> PostAsync([FromBody] PersonDTO personDTO)
        {
            var result = await _personService.CreateAsync(personDTO);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.PersonRead)]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _personService.GetAsync();
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = UserRoles.PersonRead)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _personService.GetByIdAsync(id);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut]
        [Authorize(Roles = UserRoles.PersonUpdate)]
        public async Task<IActionResult> UpdateAsync([FromBody] PersonDTO personDTO)
        {

            var result = await _personService.UpdateAsync(personDTO);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = UserRoles.PersonDelete)]
        public async Task<IActionResult> DeleteAsync(int id)
        {

            var result = await _personService.DeleteAsync(id);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet]
        [Route("paged")]
        [Authorize(Roles = UserRoles.PersonRead)]
        public async Task<ActionResult> GetPagedAsync([FromQuery] PersonFilterDb personFilterDb)
        {
            var result = await _personService.GetPagedAsync(personFilterDb);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
