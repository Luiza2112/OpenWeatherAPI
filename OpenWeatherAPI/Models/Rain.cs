using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OpenWeatherAPI.Models
{
    public class Rain
    {
        [JsonPropertyName("1h")]
        public double _1h { get; set; }
    }
}
