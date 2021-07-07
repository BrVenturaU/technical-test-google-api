using Data.DataTransferObjects.Location;
using Data.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Services.Location
{
    public interface ILocationManager
    {
        Task<(LocationDto, OuterApiResponse)> GetLocation(string ip);
    }
}
