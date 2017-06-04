using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DeboterLibrary
{
    class Location
    {
        [JsonProperty("longitude")]
        public float Longitude { get; set; }

        [JsonProperty("latitude")]
        public float Latitude { get; set; }

    }
}
