﻿using ApiDotNet6.Application.DTOs;
using ApiDotNet6.Application.Services.Interface;
using ApiDotNet6.Domain.Authentication;
using ApiDotNet6.Domain.FiltersDb;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ApiDotNet6.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : BaseController
    {
        private readonly IPersonService _personService;
        private readonly ICurrentUser _currentUser;
        private List<string> _permissionNeeded = new List<string>() { "admin" };
        private readonly List<string> _permissionUser;

        public PersonController(IPersonService personService, ICurrentUser currentUser)
        {
            _personService = personService;
            _currentUser = currentUser;
            _permissionUser = _currentUser?.Permissions?.Split(",")?.ToList() ?? new List<string>();

        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] PersonDTO personDTO)
        {
            _permissionNeeded.Add("CadastraPessoa");
            if (!ValidPermission(_permissionUser, _permissionNeeded))
                return ForBidden();

            var result = await _personService.CreateAsync(personDTO);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            _permissionNeeded.Add("BuscaPessoa");
            if (!ValidPermission(_permissionUser, _permissionNeeded))
                return ForBidden();


            var result = await _personService.GetAsync();
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            _permissionNeeded.Add("BuscaPessoa");
            if (!ValidPermission(_permissionUser, _permissionNeeded))
                return ForBidden();

            var result = await _personService.GetByIdAsync(id);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] PersonDTO personDTO)
        {
            _permissionNeeded.Add("EditaPessoa");
            if (!ValidPermission(_permissionUser, _permissionNeeded))
                return ForBidden();

            var result = await _personService.UpdateAsync(personDTO);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            _permissionNeeded.Add("DeletaPessoa");
            if (!ValidPermission(_permissionUser, _permissionNeeded))
                return ForBidden();


            var result = await _personService.DeleteAsync(id);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet]
        [Route("paged")]
        public async Task<ActionResult> GetPagedAsync([FromQuery] PersonFilterDb personFilterDb)
        {
            var result = await _personService.GetPagedAsync(personFilterDb);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
