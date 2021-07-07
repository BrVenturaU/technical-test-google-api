using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DataTransferObjects.Location
{
    public class LocationDto
    {
        public string Ip { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
        public string Loc { get; set; }
        public string Timezone { get; set; }
        public string Latitude { 
            get {
                return Loc.Split(',').FirstOrDefault();
            } 
        }
        public string Longitude
        {
            get
            {
                return Loc.Split(',').LastOrDefault();
            }
        }
    }
}
