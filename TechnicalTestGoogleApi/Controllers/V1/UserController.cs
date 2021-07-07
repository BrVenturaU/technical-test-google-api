using AutoMapper;
using Contracts.Repositories;
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
using TechnicalTestGoogleApi.Utils;

namespace TechnicalTestGoogleApi.Controllers.V1
{
    [ApiController]
    [Route("api/v1/user")]
    [Authorize]
    public class UserController: ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        // Se creo el repositorio para darle uso a la lógica.
        // Sin embargo Identity ya provee toda la lógica necesaria para el usuario (esto se tiene en cuenta).
        public UserController(IRepositoryManager repositoryManager, UserManager<User> userManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        /// <summary>
        /// Busca el usuario mediante la sesión.
        /// </summary>
        /// <returns>Los datos del usuario en sesión.</returns>
        /// <response code="200">El usuario actual en sesión.</response>
        /// <response code="401">Sesión de usuario inactiva.</response>
        /// <response code="404">Si el usuario no se ha encontrado.</response>
        [HttpGet("profile")]
        [ProducesResponseType(typeof(UserDto), (int) HttpStatusCode.OK)]
        public async Task<ActionResult<UserDto>> GetProfile()
        {
            var userId = HttpContext.User.Claims.Where(claim => claim.Type == "identifier").FirstOrDefault()?.Value;
            var user = await _repositoryManager.User.GetProfile(userId, false);
            if (user == null)
                return ApiResponse.NotFound("El perfil del usuario no ha sido encontrado.");
            var userDto = _mapper.Map<UserDto>(user);
            return ApiResponse.Ok(userDto, "Perfil del usuario.");
        }


        /// <summary>
        /// Actualiza los datos del perfil del usuario.
        /// </summary>
        /// <param name="userUpdateDto">Datos del usuario disponibles para ser actualizados</param>
        /// <returns>El usuario actualizado.</returns>
        /// <response code="200">El usuario con los nuevos datos.</response>
        /// <response code="400-1">El nombre de usuario pertenece a otro usuario.</response>
        /// <response code="400-2">Los datos de actualización del usuario contienen errores.</response>
        /// <response code="401">Sesión de usuario inactiva.</response>
        /// <response code="404">Si el usuario no se ha encontrado.</response>
        [HttpPut("profile")]
        public async Task<ActionResult<UserDto>> UpdateProfile([FromBody] UserUpdateDto userUpdateDto)
        {
            var userId = HttpContext.User.Claims.Where(claim => claim.Type == "identifier").FirstOrDefault()?.Value;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return ApiResponse.NotFound("El perfil del usuario no ha sido encontrado.");
            var (existsUserName, isActualUserName) = await _repositoryManager.User.ExistsUserName(userId, userUpdateDto.UserName, false);
            if (existsUserName && !isActualUserName)
                return ApiResponse.BadRequest("El nombre de usuario ya existe. Pruebe con otro nombre.");
            user = _mapper.Map(userUpdateDto, user);

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
                return result.GetIdentityErrors().response;

            var passwordResult = await _userManager.ChangePasswordAsync(user, userUpdateDto.CurrentPassword, userUpdateDto.Password);
            if (!passwordResult.Succeeded)
                return passwordResult.GetIdentityErrors().response;
            var userDto = _mapper.Map<UserDto>(user);
            return ApiResponse.Ok(userDto, "Usuario actualizado con éxito");
        }
    }
}
