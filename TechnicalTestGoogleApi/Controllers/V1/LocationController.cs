using Contracts.Services.Location;
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
    public class LocationController : ControllerBase
    {
        private readonly ILocationManager _locationManager;

        public LocationController(ILocationManager locationManager)
        {
            _locationManager = locationManager;
        }

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
