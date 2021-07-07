using Contracts.Repositories;
using Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public UserController(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        [HttpGet("profile")]
        public async Task<ActionResult<User>> GetProfile()
        {
            var userId = HttpContext.User.Claims.Where(claim => claim.Type == "identifier").FirstOrDefault()?.Value;
            var user = await _repositoryManager.User.GetProfile(userId, false);
            return ApiResponse.Ok(user, "Perfil del usuario.");
        }

        [HttpPut("profile")]
        public ActionResult UpdateProfile()
        {
            return Ok();
        }
    }
}
