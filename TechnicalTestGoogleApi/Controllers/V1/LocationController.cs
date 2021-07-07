using Contracts.Services.Location;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TechnicalTestGoogleApi.Utils;

namespace TechnicalTestGoogleApi.Controllers.V1
{
    [ApiController]
    [Route("api/v1/location")]
    [Authorize]
    public class LocationController : ControllerBase
    {
        private readonly ILocationManager _locationManager;

        public LocationController(ILocationManager locationManager)
        {
            _locationManager = locationManager;
        }

        /// <summary>
        /// Obtiene datos de la ubicación del usuario a traves de la IP.
        /// </summary>
        /// <param name="ip">IP del dispositivo del usuario.</param>
        /// <returns>Datos de la geolocalización del usuario.</returns>
        /// <response code="200">La geolocalización del usuario.</response>
        /// <response code="404">Si la IP no se encuentra disponible.</response>

        [HttpGet("{ip}")]
        public async Task<ActionResult> GetLocation(string ip)
        {
            var (location, response) = await _locationManager.GetLocation(ip);
            if (!response.IsSuccessResponse())
                return (ApiResponse)response;
            return ApiResponse.Ok(location, response.Message);
        }
    }
}
