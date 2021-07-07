using Contracts.Services.Location;
using Data.DataTransferObjects.Location;
using Data.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.LocationServices
{
    public class LocationManager : ILocationManager
    {
        public async Task<(LocationDto, OuterApiResponse)> GetLocation(string ip)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync($"https://ipinfo.io/{ip}");
            if (!response.IsSuccessStatusCode)
                return (null, OuterApiResponse.NotFoundResponse("La dirección IP no ha sido encontrada."));
            var body = await response.Content.ReadAsStringAsync();
            var locationDto = JsonConvert.DeserializeObject<LocationDto>(body);
            return (locationDto, OuterApiResponse.SuccessResponse("Ubicación conseguida."));
        }
    }
}
