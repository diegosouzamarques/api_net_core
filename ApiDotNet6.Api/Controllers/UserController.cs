using ApiDotNet6.Application.DTOs;
using ApiDotNet6.Application.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace ApiDotNet6.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> RegisterAsync([FromForm] UserDTO userDTO)
        {

            var result = await _userService.Register(userDTO);
            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result);

        }

        [HttpPost]
        [Route("signin")]
        public async Task<ActionResult> SigninAsync([FromForm] UserSigninDTO userDTO)
        {

            var result = await _userService.Signin(userDTO);
            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result);

        }

        [HttpPost]
        [Route("refreshtoken")]
        public async Task<ActionResult> RefreshTokenAsync([FromForm] RefreshTokenDTO refreshTokenDTO)
        {

            var result = await _userService.RefreshToken(refreshTokenDTO);
            if (result.IsSuccess)
                return Ok(result.Data);

            return BadRequest(result);

        }
    }
}
