﻿using ApiDotNet6.Application.DTOs;
using ApiDotNet6.Application.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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
        public async Task<ActionResult> CreateImageBase64Async(PersonImageDTO personImageDTO)
        {
            var result = await _personImageService.CreateImageBase64Async(personImageDTO);
           if(result.IsSuccess)
                return Ok(result);

           return BadRequest(result);
        }

        [HttpPost]
        [Route("pathimage")]
        [Authorize(Roles = UserRoles.ImgBytesCreate)]
        public async Task<ActionResult> CreateImageAsync(PersonImageDTO personImageDTO)
        {
            var result = await _personImageService.CreateImageAsync(personImageDTO);
            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(result);
        }


    }
}
