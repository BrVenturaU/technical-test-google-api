using AutoMapper;
using Contracts.Repositories;
using Data.DataTransferObjects.User;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TechnicalTestGoogleApi.Utils;

namespace TechnicalTestGoogleApi.Controllers.V1
{
    [ApiController]
    [Route("api/v1/user")]
    [Authorize]
    public class UserController: ControllerBase
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public UserController(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

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

        [HttpPut("profile")]
        public ActionResult UpdateProfile()
        {
            return Ok();
        }
    }
}
