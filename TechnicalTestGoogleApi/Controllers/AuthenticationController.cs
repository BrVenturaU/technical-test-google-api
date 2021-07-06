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
using System.Threading.Tasks;

namespace TechnicalTestGoogleApi.Controllers
{
    [ApiController]
    [Route("api/authentication")]
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

        [HttpPost("login")]
        public async Task<ActionResult> Authenticate([FromBody] UserForAuthenticationDto userForAuthentication)
        {
            var isValidUser = await _authenticationManager.ValidateUser(userForAuthentication);
            if (!isValidUser)
                return NotFound("El usuario o la contraseña son incorrectos.");
            return Ok(new { Token = _authenticationManager.CreateToken() });
        }

        [HttpGet("refresh"), Authorize]
        public async Task<ActionResult> RefreshSessionToken()
        {
            var userId = HttpContext.User.Claims.Where(claim => claim.Type == "identifier").FirstOrDefault()?.Value;
            var existsUser = await _authenticationManager.ExistsUser(new UserForRefreshSession { Id = userId });
            if (!existsUser)
                return Forbid("Acceso denegado.");

            return Ok(new { Token = _authenticationManager.CreateToken() });
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> RegisterUser([FromBody] UserForCreationDto userForCreationDto)
        {
            var user = _mapper.Map<User>(userForCreationDto);
            var result = await _userManager.CreateAsync(user, userForCreationDto.Password);
            if (!result.Succeeded)
            {
                result.Errors.ToList().ForEach(error => ModelState.TryAddModelError(error.Code, error.Description));
                return BadRequest(ModelState);
            }

            var userDto = _mapper.Map<UserDto>(user);
            return StatusCode(201, userDto);
        }


    }
}
