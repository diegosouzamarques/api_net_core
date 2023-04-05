using ApiDotNet6.Application.DTOs;
using ApiDotNet6.Application.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;

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


        #region Documentation
        // POST api/User
        /// <summary>
        /// Cria um usuário na entidade User
        /// </summary>
        /// <response code="200">
        /// Retorno será um usuário criado com sucesso 
        /// </response>
        /// <response code="400">Retorno com descrição do esta faltando na requisição
        /// </response>  
        #endregion
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> RegisterAsync([FromForm] UserDTO userDTO)
        {
            try
            {
                var result = await _userService.Register(userDTO);
                if (result.IsSuccess)
                    return Ok(result.Data);

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                var result = StatusCode(StatusCodes.Status500InternalServerError, ex.GetaAllMessages());
                return result;
            }
        }

        #region Documentation
        // POST api/User/signin
        /// <summary>
        /// Gera um token com as permissões para que o usuário seja autenticado e autorizado
        /// </summary>
        /// <remarks name="basicAutenticate">Nome e senha de usuário [name:pass] em base64</remarks>
        /// <response code="200">
        /// Retorno será token de acesso e refreshToken 
        /// </response>
        /// <response code="400">Retorno com descrição do esta faltando na requisição
        /// </response>  
        #endregion
        [HttpPost]
        [Route("signin")]
        public async Task<ActionResult> SigninAsync([FromForm] UserSigninDTO userDTO)
        {
            try {

                var result = await _userService.Signin(userDTO);
                if (result.IsSuccess)
                    return Ok(result.Data);

                return BadRequest(result);

            }
            catch(Exception ex)
            {
                var result = StatusCode(StatusCodes.Status500InternalServerError, ex.GetaAllMessages());
                return result;
            }

        }

        #region Documentation
        // POST api/User/refreshtoken
        /// <summary>
        /// Gera um novo token com as permissões para que o usuário seja autenticado e autorizado
        /// </summary>
        /// <response code="200">
        /// Retorno será um novo token de acesso e refreshToken 
        /// </response>
        /// <response code="400">Retorno com descrição do esta faltando na requisição
        /// </response>  
        #endregion
        [HttpPost]
        [Route("refreshtoken")]
        public async Task<ActionResult> RefreshTokenAsync([FromForm] RefreshTokenDTO refreshTokenDTO)
        {
            try
            {
                var result = await _userService.RefreshToken(refreshTokenDTO);
                if (result.IsSuccess)
                    return Ok(result.Data);

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                var result = StatusCode(StatusCodes.Status500InternalServerError, ex.GetaAllMessages());
                return result;
            }

        }


        #region Documentation
        // Get api/User/getpermission
        /// <summary>
        /// Busca todas permissões cadastradas
        /// </summary>
        /// <response code="200">
        /// Retorno será uma lista com as permissões existentes
        /// </response>
        /// <response code="400">Retorno com descrição do esta faltando na requisição
        /// </response>  
        #endregion
        [HttpGet]
        [Route("getpermission")]
        public async Task<ActionResult> GetAsync()
        {
            try
            {
                var result = await _userService.PermissionAsync();
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
