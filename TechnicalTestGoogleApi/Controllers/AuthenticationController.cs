using AutoMapper;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.DataTransferObjects.User;
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
        private readonly IMapper _mapper;

        public AuthenticationController(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
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
