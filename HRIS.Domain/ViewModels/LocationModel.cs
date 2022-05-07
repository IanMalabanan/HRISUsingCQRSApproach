using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HRIS.Application.Common.ViewModels
{
    public class LocationModel
    {
        [JsonProperty("ip")]
        public string ip { get; set; }

        [JsonProperty("country")]
        public string country { get; set; }

        [JsonProperty("countryName")]
        public string countryName { get; set; }

        [JsonProperty("region")]
        public string region { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("postal")]
        public string Postal { get; set; }

        [JsonProperty("loc")]
        public string loc { get; set; }

        [JsonProperty("latitude")]
        public string Latitude { get; set; }

        [JsonProperty("longitude")]
        public string Longitude { get; set; }

        [JsonProperty("principalSubdivision")]
        public string Province { get; set; }

        [JsonProperty("locationData")]
        public Dictionary<string,string> locationData { get; set; }
    }

    public class LocationData
    {
        string countryName { get; set; }
        string countryCode { get; set; }
        string cityName { get; set; }
        string cityGeonamesId { get; set; }
        string lat { get; set; }
        string lng { get; set; }
        string tz { get; set; }
        string continentCode { get; set; }
    }
}
