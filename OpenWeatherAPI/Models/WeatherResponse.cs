using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//https://api.openweathermap.org/data/2.5/weather?q={city name}&appid={API key} -> Procurar pelo Nome da Cidade

namespace OpenWeatherAPI.Models
{
    public class WeatherResponse
    {
        public Coord Coord;
        public List<Weather> Weather;
        public string Base;
        public Main Main;
        public int Visivility;
        public Wind Wind;
        public Rain Rain;
        public Clouds Clouds;
        public int Dt;
        public Sys Sys;
        public int Timezone;
        public int Id;
        public string Name;
        public int Cod;
    }
}
