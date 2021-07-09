using AutoMapper;
using Contracts.Services.User;
using Data.DataTransferObjects.User;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TechnicalTestGoogleApi.Extensions;
using TechnicalTestGoogleApi.Filters;
using TechnicalTestGoogleApi.Utils;

namespace TechnicalTestGoogleApi.Controllers.V1
{
    [ApiController]
    [Route("api/v1/authentication")]
    //[ServiceFilter(typeof(ErrorsFilterAttribute))]
    public class AuthenticationController: ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly IMapper _mapper;

        public AuthenticationController(UserManager<User> userManager, IAuthenticationManager authenticationManager, IMapper mapper)
        {
            _userManager = userManager;
            _authenticationManager = authenticationManager;
            _mapper = mapper;
        }

        /// <summary>
        /// Autentica un usuario con sus credenciales.
        /// </summary>
        /// <param name="userForAuthentication">Objeto con los datos del usuario para iniciar sesión</param>
        /// <returns>El token de sesión del usuario.</returns>
        /// <response code="200">Un token de sesión que permite al usuario estar autenticado.</response>
        /// <response code="404">Si las credenciales del usuario son incorrectas.</response>
        [HttpPost("login")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> Authenticate([FromBody] UserForAuthenticationDto userForAuthentication)
        {
            var isValidUser = await _authenticationManager.ValidateUser(userForAuthentication);
            if (!isValidUser)
                return ApiResponse.NotFound("Sus credenciales de usuario son incorrectas.");
            return ApiResponse.Ok(new { Token = _authenticationManager.CreateToken() }, "Usuario autenticado correctamente.");
        }

        /// <summary>
        /// Refresca la sesión generando un nuevo token de sesión.
        /// </summary>
        /// <returns>El nuevo token de sesión del usuario.</returns>
        /// <response code="200">Un token de sesión que permite al usuario continuar autenticado.</response>
        /// <response code="401">Sesión de usuario inactiva</response>
        /// <response code="403">Si el usuario no existe y se deniega el acceso.</response>
        [HttpGet("refresh"), Authorize]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public async Task<ActionResult> RefreshSessionToken()
        {
            var userId = HttpContext.User.Claims.Where(claim => claim.Type == "identifier").FirstOrDefault()?.Value;
            var existsUser = await _authenticationManager.ExistsUser(new UserForRefreshSession { Id = userId });
            if (!existsUser)
                return ApiResponse.Forbidden("Acceso denegado.");

            return ApiResponse.Ok(new { Token = _authenticationManager.CreateToken() });
        }

        /// <summary>
        /// Crea un nuevo usuario.
        /// </summary>
        /// <param name="userForCreationDto">Objeto con los datos del usuario a ser creado</param>
        /// <returns>Los datos del nuevo usuario.</returns>
        /// <response code="200">El nuevo usuario creado.</response>
        /// <response code="400">Si los datos de creación del usuario son incorrectos.</response>
        [HttpPost("register")]
        [ProducesResponseType(typeof(UserDto),(int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<UserDto>> RegisterUser([FromBody] UserForCreationDto userForCreationDto)
        {
            var user = _mapper.Map<User>(userForCreationDto);
            var result = await _userManager.CreateAsync(user, userForCreationDto.Password);
            if (!result.Succeeded)
                return result.GetIdentityErrors().response;

            var userDto = _mapper.Map<UserDto>(user);
            return ApiResponse.Created(userDto, "Usuario creado con exito.");
        }


    }
}
